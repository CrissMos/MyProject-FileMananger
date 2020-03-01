using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyProject.Models
{
    public class ModelContext: DbContext
    {
        public ModelContext(DbContextOptions<ModelContext> options): base(options)
        {

        }

        public DbSet<Asset> Assets { get; set; }
        public DbSet<Folder> Folders { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Asset>().ToTable("Asset");

            base.OnModelCreating(modelBuilder);
        }

        public override int SaveChanges()
        {
            AddTimeStamps();
            return base.SaveChanges();
        }

        private void AddTimeStamps()
        {
            var entities = ChangeTracker.Entries().Where(x => x.Entity is BaseModel &&
              (x.State == EntityState.Added || x.State == EntityState.Modified || x.State == EntityState.Deleted));

            foreach (var entity in entities)
            {
                switch (entity.State)
                {
                    case EntityState.Added:
                        ((BaseModel)entity.Entity).Created = DateTime.UtcNow;
                        break;
                    case EntityState.Modified:
                        ((BaseModel)entity.Entity).Created = DateTime.UtcNow;
                        break;
                    case EntityState.Deleted:
                        ((BaseModel)entity.Entity).Created = DateTime.UtcNow;
                        break;
                    default:
                        ((BaseModel)entity.Entity).Created = DateTime.UtcNow;
                        break;
                }
            }
        }
    }
}
