using API.BLL.Specification;
using API.Models;
using API.Models.CoreModels;

namespace API.Abstractions.BLL.Core
{
    public class ProductWithFiltersForCountSpecification : BaseSpecification<Product>
    {
        public ProductWithFiltersForCountSpecification (ProductSpecParams productSpec) : base (x =>
            (string.IsNullOrEmpty (productSpec.Search) || x.Name.ToLower ().Contains (productSpec.Search)) &&
            (!productSpec.BrandId.HasValue || x.ProductBrandId == productSpec.BrandId) && (!productSpec.TypeId.HasValue || x.ProductTypeId == productSpec.TypeId)) { }
    }
}