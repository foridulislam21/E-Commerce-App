using Microsoft.AspNetCore.Mvc;
namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        [HttpGet]
        public string GetProducts()
        {
            return "Soon you will see all type of product";
        }
        [HttpGet("{id}")]
        public string GetProduct(int id){
            return "Single Product";
        }
    }
}