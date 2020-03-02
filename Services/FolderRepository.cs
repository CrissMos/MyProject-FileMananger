using MyProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyProject.Services
{
	public class FolderRepository : IFolderRepository
	{
        protected ModelContext _context { get; set; }

        public FolderRepository(ModelContext context)
        {
            _context = context;
        }

        public IEnumerable<Folder> GetRootFolders()
        {
            return _context.Folders.Where(folder => folder.ParentId==null && folder.Deleted == null).ToList();
        }

        public Folder GetFolderById(Guid Id)
        {
            return _context.Folders.Where(f => f.Id == Id && f.Deleted == null).FirstOrDefault();
        }

        public ICollection<Folder> GetByParentId(string ParentId)
        {
            return _context.Folders.Where(f => f.ParentId == ParentId && f.Deleted == null).ToList();
        }

        public bool Create(Folder folder)
        {
            try
            {
                _context.Add(folder);
                _context.SaveChanges();

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
