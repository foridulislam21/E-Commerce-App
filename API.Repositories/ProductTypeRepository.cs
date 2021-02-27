using API.Abstractions.Repository;
using API.Models;
using API.Repositories.Base;
using API.StorageCenter.Data;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories
{
    public class ProductTypeRepository : EFRepository<ProductType>, IProductTypeRepository
    {
        private readonly StoreContext _db;
        public ProductTypeRepository(DbContext db) : base(db)
        {
            _db = db as StoreContext;
        }
    }
}