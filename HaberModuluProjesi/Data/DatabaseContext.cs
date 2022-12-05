using HaberModuluProjesi.Entities;
using Microsoft.EntityFrameworkCore;

namespace HaberModuluProjesi.Data
{
    public class DatabaseContext : DbContext
    {
        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<News> News { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"server=(localdb)\MSSQLLocalDB; Database=HaberModuluProjesi; integrated security=True;");
            base.OnConfiguring(optionsBuilder);
        }

    }
}
