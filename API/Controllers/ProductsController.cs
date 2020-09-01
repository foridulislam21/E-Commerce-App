using System.Collections.Generic;
using System.Threading.Tasks;
using API.Abstractions.BLL;
using API.Models;
using Microsoft.AspNetCore.Mvc;
namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductManager _productManager;
        private readonly IProductBrandManager _brandManager;
        private readonly IProductTypeManager _typeManager;
        public ProductsController(IProductManager productManager, IProductBrandManager brandManager, IProductTypeManager typeManager)
        {
            _typeManager = typeManager;
            _brandManager = brandManager;
            _productManager = productManager;

        }
        [HttpGet]
        public async Task<ActionResult<List<Product>>> GetProducts()
        {
            var products = await _productManager.GetAll();
            return Ok(products);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProduct(int id)
        {
            var product = await _productManager.GetById(id);
            return Ok(product);
        }
        [HttpGet("brands")]
        public async Task<ActionResult<List<ProductBrand>>> GetBrands()
        {
            var brands = await _brandManager.GetAll();
            return Ok(brands);
        }
        [HttpGet("types")]
        public async Task<ActionResult<List<ProductType>>> GetTypes()
        {
            var types = await _typeManager.GetAll();
            return Ok(types);
        }
    }
}