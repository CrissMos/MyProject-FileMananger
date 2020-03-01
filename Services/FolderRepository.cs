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

        public IEnumerable<Folder> GetFolders()
        {
            return _context.Folders.Where(folder => folder.Deleted == null).ToList();
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
