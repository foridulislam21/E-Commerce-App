using System;
using System.Linq.Expressions;
using API.BLL.Specification;
using API.Models;

namespace API.Abstractions.BLL.Core {
    public class ProductsWithTypesAndBrandsSpecification : BaseSpecification<Product> {
        public ProductsWithTypesAndBrandsSpecification () {
            AddInclude (x => x.ProductType);
            AddInclude (x => x.ProductBrand);
        }

        public ProductsWithTypesAndBrandsSpecification (int id) : base (x => x.Id == id) {
            AddInclude (x => x.ProductType);
            AddInclude (x => x.ProductBrand);
        }
    }
}