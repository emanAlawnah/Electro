using EcomarceFirstApp.Models;
using Microsoft.EntityFrameworkCore;

namespace EcomarceFirstApp.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet <Product> Products { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer("Server=db31695.public.databaseasp.net; Database=db31695; User Id=db31695; Password=5x-Ge+Z9%6yK; Encrypt=True; TrustServerCertificate=True; MultipleActiveResultSets=True");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Category>().HasData(
                new Category
                {
                    Id = 1,
                    Name = "mobile",
                    Description= "this is mobile ",
                    Image = "default.jpg"

                },
                   new Category
                   {
                       Id = 2,
                       Name = "Laptop",
                       Description = "this is Laptop ",
                       Image = "default.jpg"


                   },
                      new Category
                      {
                          Id = 3,
                          Name = "Tablet",
                          Description = "this is Tablet ",
                          Image = "default.jpg"

                      }
                );
        }
    }
}
