namespace MultiTenancy.Services.TenantService.DTO
{
    public class CreateTenantRequest
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public bool Isolated { get; set; }
    }
}
