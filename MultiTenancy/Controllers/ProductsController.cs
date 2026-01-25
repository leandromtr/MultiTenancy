using Microsoft.AspNetCore.Mvc;
using MultiTenancy.Services.DTOs;
using MultiTenancy.Services.ProductService;

namespace MultiTenancy.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;


        public ProductsController(IProductService productService)
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
