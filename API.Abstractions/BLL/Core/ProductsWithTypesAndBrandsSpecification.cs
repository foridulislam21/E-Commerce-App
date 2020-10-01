using System;
using System.Linq.Expressions;
using API.BLL.Specification;
using API.Models;
using API.Models.CoreModels;

namespace API.Abstractions.BLL.Core
{
    public class ProductsWithTypesAndBrandsSpecification : BaseSpecification<Product>
    {
        public ProductsWithTypesAndBrandsSpecification (ProductSpecParams productSpec) : base (x =>
            (string.IsNullOrEmpty (productSpec.Search) || x.Name.ToLower ().Contains (productSpec.Search)) &&
            (!productSpec.BrandId.HasValue || x.ProductBrandId == productSpec.BrandId) && (!productSpec.TypeId.HasValue || x.ProductTypeId == productSpec.TypeId))
        {
            AddInclude (x => x.ProductType);
            AddInclude (x => x.ProductBrand);
            AddOrderBy (x => x.Name);
            ApplyPaging (productSpec.PageSize, (productSpec.PageSize * (productSpec.PageIndex - 1)));

            if (!string.IsNullOrEmpty (productSpec.Sort))
            {
                switch (productSpec.Sort)
                {
                    case "priceAsc":
                        AddOrderBy (p => p.Price);
                        break;
                    case "priceDesc":
                        AddOrderByDescending (p => p.Price);
                        break;
                    default:
                        AddOrderBy (n => n.Name);
                        break;
                }
            }
        }

        public ProductsWithTypesAndBrandsSpecification (int id) : base (x => x.Id == id)
        {
            AddInclude (x => x.ProductType);
            AddInclude (x => x.ProductBrand);
        }
    }
}