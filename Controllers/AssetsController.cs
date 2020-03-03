using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyProject.Models;
using MyProject.Models.ApiModels;
using MyProject.Services;

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

        public AssetsController(
            IAssetRepository assetRepository,
            IFolderRepository folderRepository,
            IVariantRepository variantRepository
            )
        {
            _assetRepository = assetRepository;
            _folderRepository = folderRepository;
            _variantRepository = variantRepository;
        }

        [HttpPost("create")]
        public IActionResult CreateAsset(ApiAsset apiAsset)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var asset = (Asset)apiAsset;

            var assetDb = _assetRepository.Create(asset);

            if (!assetDb)
            {
                return BadRequest();
            }

            return Ok(assetDb);

            //Asset asset = new Asset();
            //asset.Category = Enums.AssetCategory.image;
            //asset.Name = "Image1";

            //var assetDb = _assetRepository.Create(asset);

            //if (!assetDb)
            //{
            //    return BadRequest();
            //}

            //return Ok(asset);
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