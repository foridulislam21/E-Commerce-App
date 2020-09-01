using API.Abstractions.Repository;
using API.Models;
using API.Repositories.Base;
using API.StorageCenter;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories
{
    public class ProductRepository : EFRepository<Product>, IProductRepository
    {
        private readonly StoreContext _db;
        public ProductRepository(DbContext db) : base(db)
        {
            _db = db as StoreContext;
        }
    }
}