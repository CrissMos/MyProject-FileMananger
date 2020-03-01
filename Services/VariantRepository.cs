﻿using Microsoft.EntityFrameworkCore;
using MyProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyProject.Services
{
	public class VariantRepository : IVariantRepository
	{
        protected ModelContext _context { get; set; }

        public VariantRepository(ModelContext context)
        {
            _context = context;
        }

        public IEnumerable<Variant> GetVariantsByAssetId(Guid AssetId)
        {
            return _context.Variants.Include(v => v.Asset).Where(v => v.Asset.Id == AssetId && v.Deleted == null).ToList();
        }

        public bool Create(Version version)
        {
            try
            {
                _context.Add(version);
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
