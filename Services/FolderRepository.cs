﻿using MyProject.Models;
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
            return _context.Folders.Where(folder => folder.ParentId==null && folder.Deleted == null).ToList();
        }

        public Folder GetFolderByParentId(Guid ParentId)
        {
            return _context.Folders.Where(f => f.Id == ParentId && f.Deleted == null).FirstOrDefault();
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