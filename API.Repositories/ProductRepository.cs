using System.Collections.Generic;
using System.Threading.Tasks;
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
        public override async Task<IReadOnlyList<Product>> GetAll()
        {
            return await _db.Products
            .Include(b => b.ProductBrand)
            .Include(t => t.ProductType)
            .ToListAsync();
        }
        public override async Task<Product> GetById(int id) {
            return await _db.Products
            .Include(b => b.ProductBrand)
            .Include(t => t.ProductType)
            .FirstOrDefaultAsync(p => p.Id == id);
        }
    }
}