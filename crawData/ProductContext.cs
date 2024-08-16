using crawData;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Pomelo.EntityFrameworkCore.MySql.Storage;
using System.Configuration;

namespace CrawData
{
    public class ProductContext : DbContext
    {
        public ProductContext() : base(GetDbContextOptions())
        {
        }

        public DbSet<Product> Products { get; set; }

        private static DbContextOptions<ProductContext> GetDbContextOptions()
        {
            var connectionString = ConfigurationManager.ConnectionStrings["ProductDatabase"].ConnectionString;
            var optionsBuilder = new DbContextOptionsBuilder<ProductContext>();
            optionsBuilder.UseMySql(connectionString);
            return optionsBuilder.Options;
        }
    }
}
