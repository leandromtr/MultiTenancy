using Microsoft.EntityFrameworkCore;
using MultiTenancy.Models;

namespace MultiTenancy.Services
{
    public class CurrentTenantService: ICurrentTenantService
    {
        private readonly ApplicationDbContext _context;

        public CurrentTenantService(ApplicationDbContext context)
        {
            _context = context;
        }

        public string? TenantId { get; set; }

        public async Task<bool> SetTenant(string tenantId)
        {
            var tenantInfo = await _context.Tenants.Where(x => x.Id == tenantId).FirstOrDefaultAsync();
            if (tenantInfo != null)
            {
                TenantId = tenantId;
                return true;
            }
            else
            {
                throw new Exception("Tenant invalid");
            }
        }
    }
}
