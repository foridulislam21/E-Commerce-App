using System.Collections.Generic;
using System.Threading.Tasks;
using API.Abstractions.BLL;
using API.Abstractions.BLL.Core;
using API.Configurations.Error;
using API.Models;
using API.Models.DTO;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
namespace API.Controllers {
    public class ProductsController : BaseApiController {
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
            var spec = new ProductsWithTypesAndBrandsSpecification ();
            var products = await _productManager.ListAsync (spec);
            var productsFromRepo = _mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductToReturnDto>> (products);
            return Ok (productsFromRepo);
        }

        [HttpGet ("{id}")]
        [ProducesResponseType (StatusCodes.Status200OK)]
        [ProducesResponseType (typeof (ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetProduct (int id) {
            var spec = new ProductsWithTypesAndBrandsSpecification (id);

            var product = await _productManager.GetEntityWithSpec (spec);
            var productFromrepo = _mapper.Map<Product, ProductToReturnDto> (product);
            if (productFromrepo == null) {
                return NotFound (new ApiResponse (404));
            }
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