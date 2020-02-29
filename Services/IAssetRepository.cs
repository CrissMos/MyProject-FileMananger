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
        bool Create(Asset asset);
    }
}
