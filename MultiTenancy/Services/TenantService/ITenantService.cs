using MultiTenancy.Models;
using MultiTenancy.Services.TenantService.DTO;

namespace MultiTenancy.Services
{
    public interface ITenantService
    {
        //IEnumerable<Tenant> GetAllTenants();
        Tenant CreateTenant(CreateTenantRequest request);
        //bool DeleteTenant(int id);

    }
}
