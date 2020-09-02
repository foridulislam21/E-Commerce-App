using System.Collections.Generic;
using System.Threading.Tasks;
using API.Abstractions.BLL;
using API.Models;
using API.Models.DTO;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
namespace API.Controllers {
    [ApiController]
    [Route ("api/[controller]")]
    public class ProductsController : ControllerBase {
        private readonly IProductManager _productManager;
        private readonly IProductBrandManager _brandManager;
        private readonly IProductTypeManager _typeManager;
        private readonly IMapper _mapper;
        public ProductsController (IProductManager productManager, IProductBrandManager brandManager, IProductTypeManager typeManager, IMapper mapper) {
            _mapper = mapper;
            _typeManager = typeManager;
            _brandManager = brandManager;
            _productManager = productManager;

        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<ProductToReturnDto>>> GetProducts () {
            var products = await _productManager.GetAll ();
            var productsFromRepo = _mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductToReturnDto>> (products);
            return Ok (productsFromRepo);
        }

        [HttpGet ("{id}")]
        public async Task<IActionResult> GetProduct (int id) {
            var product = await _productManager.GetById (id);
            var productFromrepo = _mapper.Map<Product, ProductToReturnDto> (product);
            return Ok (productFromrepo);
        }

        [HttpGet ("brands")]
        public async Task<ActionResult<List<ProductBrand>>> GetBrands () {
            var brands = await _brandManager.GetAll ();
            return Ok (brands);
        }

        [HttpGet ("types")]
        public async Task<ActionResult<List<ProductType>>> GetTypes () {
            var types = await _typeManager.GetAll ();
            return Ok (types);
        }
    }
}