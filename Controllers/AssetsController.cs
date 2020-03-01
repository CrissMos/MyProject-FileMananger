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

        public AssetsController(IAssetRepository assetRepository)
        {
            _assetRepository = assetRepository;
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
    }
}