using Microsoft.AspNetCore.Mvc;
using MultiTenancy.Services;
using MultiTenancy.Services.TenantService.DTO;

namespace MultiTenancy.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TenantsController : ControllerBase
    {
        private readonly ITenantService _tenantService;

        public TenantsController(ITenantService tenantService)
        {
            _tenantService = tenantService;
        }

        public ITenantService TenantService { get; }

        // Create a new tenant
        [HttpPost]
        public IActionResult Post(CreateTenantRequest request)
        {
            var result = _tenantService.CreateTenant(request);
            return Ok(result);
        }
    }
}
