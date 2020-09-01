using API.Abstractions.BLL;
using API.Abstractions.Repository;
using API.BLL.Base;
using API.Models;

namespace API.BLL
{
    public class ProductTypeManager : Manager<ProductType>, IProductTypeManager
    {
        private readonly IProductTypeRepository _typeRepository;
        public ProductTypeManager(IProductTypeRepository typeRepository) : base(typeRepository)
        {
            _typeRepository = typeRepository;
        }
    }
}