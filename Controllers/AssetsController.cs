using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using MyProject.Models;
using MyProject.Models.ApiModels;
using MyProject.Services;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.Extensions.Configuration;

namespace MyProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("AllowAll")]
    public class AssetsController : Controller
    {
        private readonly IAssetRepository _assetRepository;
        private readonly IFolderRepository _folderRepository;
        private readonly IVariantRepository _variantRepository;
        private readonly IConfiguration _configuration;

        public AssetsController(
            IAssetRepository assetRepository,
            IFolderRepository folderRepository,
            IVariantRepository variantRepository,
            IConfiguration configuration
            )
        {
            _assetRepository = assetRepository;
            _folderRepository = folderRepository;
            _variantRepository = variantRepository;
            _configuration = configuration;
        }

        [HttpPost("upload/{folderId}"), DisableRequestSizeLimit]
        public async Task<IActionResult> UploadFile(Guid folderId)
        {
            Asset asset = new Asset();

            if (folderId != null)
            {
                asset.FolderId = folderId;
            }
           
            asset.Category = Enums.AssetCategory.Image;

            var assetDb = _assetRepository.Create(asset);

            if (assetDb == null)
            {
                return BadRequest("Asset didn't saved.");
            }

            var date = assetDb.Created.ToShortDateString();

            Variant variant = new Variant();

            variant.AssetId = assetDb.Id;
            variant.Type = Enums.VariantType.Initial;

            var storageConnectionString = _configuration["BlobConnection"];
            CloudStorageAccount account = CloudStorageAccount.Parse(storageConnectionString);
            CloudBlobClient client = account.CreateCloudBlobClient();
            CloudBlobContainer container = client.GetContainerReference("images");

            var file = Request.Form.Files[0];
        
            string fileName= date + assetDb.Id + Enums.VariantType.Initial;

            CloudBlockBlob photo = container.GetBlockBlobReference(Path.GetFileName(fileName));

            photo.Properties.ContentType = file.ContentType;

            await photo.UploadFromStreamAsync(file.OpenReadStream());

            variant.Link =photo.Uri.ToString();

            var variantDb = _variantRepository.Create(variant);

            if (!variantDb)
            {
                return BadRequest("Variant didn't saved.");
            }

            return Ok();
        }

        [HttpPost("create")]
        public IActionResult CreateAsset(ApiAsset apiAsset)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var asset = (Asset)apiAsset;

            if (apiAsset.FolderId != null)
            {
            asset.FolderId = new Guid(apiAsset.FolderId);
            }

            var assetDb = _assetRepository.Create(asset);

            if (assetDb==null)
            {
                return BadRequest();
            }

            List<Enums.VariantType> types = new List<Enums.VariantType>();

            foreach(Enums.VariantType type in Enum.GetValues(typeof(Enums.VariantType)))
            {
                var date = assetDb.Created.ToShortDateString();

                Variant variant = new Variant();
                variant.AssetId = assetDb.Id;
                variant.Type = type;
                variant.Link = date + assetDb.Name + type;

                var variantDb = _variantRepository.Create(variant);
                if (!variantDb)
                {
                    return BadRequest("Variant didn't saved.");
                }
            }

            return Ok(true);
        }

        [HttpGet("get-assets")]
        public IActionResult GetAssets()
        {
            var assets = _assetRepository.GetAssets();

            var apiAssets = assets.Select(a => (ApiAsset)a);

            if (apiAssets == null)
            {
                return BadRequest();
            }

            return Ok(apiAssets);           
        }

        [HttpGet("thumbnail-view/{folderId}")]
        public IActionResult GetThumbnailsInFolder(Guid folderId)
        {
            var folder = _folderRepository.GetFolderById(folderId);

            if (folder == null)
            {
                return NotFound();
            }

            var assets = _assetRepository.GetAssetsByFolderId(folderId);

            if (assets == null)
            {
                return NoContent();
            }

            var apiAssets = assets.Select(a => (ApiAsset)a).ToList();

            foreach(ApiAsset a in apiAssets)
            {
                var thumbnailVariant = _variantRepository.GetThumbnailByAssetId(a.Id.GetValueOrDefault());

                if(thumbnailVariant!=null)
                {
                    a.VariantToDisplayLink = thumbnailVariant.Link;
                }
            }

            return Ok(apiAssets);
        }
    }
}