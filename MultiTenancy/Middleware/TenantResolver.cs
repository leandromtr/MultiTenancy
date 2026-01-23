using MultiTenancy.Services;

namespace MultiTenancy.Middleware
{
    public class TenantResolver
    {
        private readonly RequestDelegate _next;
        public TenantResolver(RequestDelegate next)
        {
            _next = next;
        }
        public async Task InvokeAsync(HttpContext context, ICurrentTenantService currentTenantService)
        {
            context.Request.Headers.TryGetValue("tenant", out var tenantFromHeader);
            if (string.IsNullOrEmpty(tenantFromHeader) == false)
            {
                await currentTenantService.SetTenant(tenantFromHeader);
            }
            else
            {
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                await context.Response.WriteAsync("Tenant ID is missing.");
                return;
            }
            await _next(context);
        }
    }
}
