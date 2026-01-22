using MultiTenancy.Models;
using MultiTenancy.Services.DTOs;

namespace MultiTenancy.Services
{
    public class ProductService : IProductService
    {
        private readonly ApplicationDbContext _context;

        public ProductService(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Product> GetAllProducts()
        {
            var products = _context.Products.ToList();
            return products;
        }

        public Product GetProductsById(int id)
        {
            var product = _context.Products.Where(p => p.Id == id).FirstOrDefault();
            return product;
        }

        public Product CreateProduct(CreateProductRequest request)
        {
            var product = new Product();
            product.Name = request.Name;

            _context.Add(product);
            _context.SaveChanges();

            return product;
        }

        public bool DeleteProduct(int id)
        {
            var product = _context.Products.Where(x => x.Id == id).FirstOrDefault();
            if (product == null)
            {
                return false;
            }
            _context.Remove(product);
            _context.SaveChanges();
            return true;
        }

    }
}
