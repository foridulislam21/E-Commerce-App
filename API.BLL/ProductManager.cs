using API.Abstractions.BLL;
using API.Abstractions.Repository;
using API.BLL.Base;
using API.Models;

namespace API.BLL
{
    public class ProductManager : Manager<Product>, IProductManager
    {
        private new readonly IProductRepository _repo;
        public ProductManager(IProductRepository repo) : base(repo)
        {
            _repo = repo;

        }
    }
}