using Microsoft.AspNetCore.Mvc;
using MultiTenancy.Services;
using MultiTenancy.Services.DTOs;

namespace MultiTenancy.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;


        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var products = _productService.GetAllProducts();
            return Ok(products);
        }

        [HttpPost]
        public IActionResult Post([FromBody] CreateProductRequest request)
        {
            var result = _productService.CreateProduct(request);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _productService.DeleteProduct(id);
            return NoContent();
        }
    }
}
