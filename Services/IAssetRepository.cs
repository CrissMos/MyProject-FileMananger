using MyProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyProject.Services
{
    public interface IAssetRepository
    {
        IEnumerable<Asset> GetAssets();

        IEnumerable<Asset> GetAssetsByFolderId(Guid folderId);
        bool Create(Asset asset);
    }
}
