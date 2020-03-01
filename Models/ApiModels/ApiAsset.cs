using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MyProject.Models.ApiModels
{
    public class ApiAsset
    {
        public Guid? Id { get; set; }

        public Enums.AssetCategory Category { get; set; }

        public string Name { get; set; }

        public string FolderId { get; set; }

        public static explicit operator ApiAsset(Asset asset)
        {
            ApiAsset apiAsset = new ApiAsset();

            apiAsset.Id = asset.Id;
            apiAsset.Category = asset.Category;
            apiAsset.Name = asset.Name;
            apiAsset.FolderId = asset.FolderId.ToString();

            return apiAsset;
        }
    }
}
