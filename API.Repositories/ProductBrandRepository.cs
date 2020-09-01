using API.Abstractions.Repository;
using API.Models;
using API.Repositories.Base;
using API.StorageCenter;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories
{
    public class ProductBrandRepository : EFRepository<ProductBrand>, IProductBrandRepository
    {
        private readonly StoreContext _db;
        public ProductBrandRepository(DbContext db) : base(db)
        {
            _db = db as StoreContext;
        }
    }
}