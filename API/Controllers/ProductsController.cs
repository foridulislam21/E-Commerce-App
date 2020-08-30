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
        public ProductsController(IProductManager productManager)
        {
            _productManager = productManager;

        }
        [HttpGet]
        public async Task<ICollection<Product>> GetProducts()
        {
            var products = await _productManager.GetAll();
            return products;
        }
        [HttpGet("{id}")]
        public string GetProduct(int id)
        {
            return "Single Product";
        }
    }
}