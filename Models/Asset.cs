using MyProject.Models.ApiModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyProject.Models
{
    public class Asset : BaseModel
    {
        public Guid Id { get; set; }

        public Enums.AssetCategory Category { get; set; }

        public string Name { get; set; }

        public static explicit operator Asset(ApiAsset apiAsset)
        {
            Asset asset = new Asset();

            asset.Category = apiAsset.Category;
            asset.Name = apiAsset.Name;

            return asset;
        }
    }
}
