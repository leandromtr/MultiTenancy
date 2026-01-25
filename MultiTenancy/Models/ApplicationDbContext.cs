using Microsoft.EntityFrameworkCore;
using MultiTenancy.Services;

namespace MultiTenancy.Models
{
    public class ApplicationDbContext : DbContext
    {
        private readonly ICurrentTenantService _currentTenantService;
        public string CurrentTenantId { get; set; }
        public string CurrentTenantConnectionString{ get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, ICurrentTenantService currentTenantService)
            : base(options)
        {
            _currentTenantService = currentTenantService;
            CurrentTenantId = _currentTenantService.TenantId;
            CurrentTenantConnectionString = _currentTenantService.ConnectionString;
        }

        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().HasQueryFilter(x => x.TenantId == CurrentTenantId);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string tenantConnectionString = CurrentTenantConnectionString;
            if (!string.IsNullOrEmpty(tenantConnectionString))
            {
                _ = optionsBuilder.UseSqlServer(CurrentTenantConnectionString);
            }
        }

        public override int SaveChanges()
        {
            foreach (var entry in ChangeTracker.Entries<IMustHaveTenant>().ToList())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                    case EntityState.Modified:
                        entry.Entity.TenantId = CurrentTenantId;
                        break;
                }
            }
            var result = base.SaveChanges();
            return result;
        }
    }
}
