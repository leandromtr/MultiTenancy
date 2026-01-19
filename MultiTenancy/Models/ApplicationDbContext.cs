using Microsoft.EntityFrameworkCore;

namespace MultiTenancy.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

         public DbSet<Product> Products { get; set; }
    }
}
