using API.Abstractions.BLL;
using API.Abstractions.Repository;
using API.BLL.Base;
using API.Models;

namespace API.BLL
{
    public class ProductBrandManager : Manager<ProductBrand>, IProductBrandManager
    {
        private readonly IProductBrandRepository _brandRepository;
        public ProductBrandManager(IProductBrandRepository brandRepository) : base(brandRepository)
        {
            _brandRepository = brandRepository;

        }
    }
}