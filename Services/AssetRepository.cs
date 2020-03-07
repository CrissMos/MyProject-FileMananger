using MyProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyProject.Services
{
    public class AssetRepository: IAssetRepository
    {
        protected ModelContext _context { get; set; }
        
        public AssetRepository(ModelContext context)
        {
            _context = context;
        }

        public IEnumerable<Asset> GetAssets()
        {
            return _context.Assets.Where(a => a.FolderId == null && a.Deleted == null).ToList();
        }

        public IEnumerable<Asset> GetAssetsByFolderId(Guid folderId)
        {
            return _context.Assets.Where(a => a.FolderId == folderId && a.Deleted == null).ToList();
        }

        public Asset Create(Asset asset)
        {
            try
            {
                _context.Add(asset);
                _context.SaveChanges();

                return asset;
            }
            catch
            {
                return null;
            }
        }
    }

    //public interface IAssetRepository
    //{
    //    bool Create(Asset asset);
    //}
}
