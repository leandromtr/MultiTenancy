using MultiTenancy.Models;
using MultiTenancy.Services.DTOs;

namespace MultiTenancy.Services.ProductService
{
    public interface IProductService
    {
        IEnumerable<Product> GetAllProducts();
        Product CreateProduct(CreateProductRequest request);
        bool DeleteProduct(int id);

    }
}
