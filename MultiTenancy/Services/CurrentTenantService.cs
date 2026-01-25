using Microsoft.EntityFrameworkCore;
using MultiTenancy.Models;

namespace MultiTenancy.Services
{
    public class CurrentTenantService: ICurrentTenantService
    {
        private readonly TenantDbContext _context;
        public string? TenantId { get; set; }
        public string? ConnectionString { get; set; } = null;

        public CurrentTenantService(TenantDbContext context)
        {
            _context = context;
        }

        public async Task<bool> SetTenant(string tenant)
        {
            var tenantInfo = await _context.Tenants.Where(x => x.Id == tenant).FirstOrDefaultAsync();
            if (tenantInfo != null)
            {
                TenantId = tenantInfo.Id;
                ConnectionString = tenantInfo.ConnectionString;
                return true;
            }
            else
            {
                return false;
                //throw new Exception("Tenant invalid");
            }
        }
    }
}
