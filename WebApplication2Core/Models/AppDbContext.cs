using Microsoft.EntityFrameworkCore;

namespace WebApplication2Core.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> contextOptions)
            : base(contextOptions)
        {

        }
        public DbSet<Product> products { get; set; }
    }
}
