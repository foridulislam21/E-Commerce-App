using System.Collections.Generic;
using System.Threading.Tasks;
using API.Abstractions.Repository.Base;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories.Base
{
    public class EFRepository<T> : IRepository<T> where T : class
    {
        private readonly DbContext _db;
        public EFRepository(DbContext db)
        {
            _db = db;
        }
        public virtual async Task<bool> Add(T entity)
        {
            await _db.Set<T>().AddAsync(entity);
            return await _db.SaveChangesAsync() > 0;
        }

        public virtual async Task<IReadOnlyList<T>> GetAll()
        {
            return await _db.Set<T>().ToListAsync();
        }

        public virtual async Task<T> GetById(int id)
        {
            return await _db.Set<T>().FindAsync(id);
        }

        public virtual async Task<bool> Remove(T entity)
        {
            _db.Set<T>().Remove(entity);
            return await _db.SaveChangesAsync() > 0;
        }

        public virtual async Task<bool> Update(T entity)
        {
            _db.Set<T>().Update(entity);
            return await _db.SaveChangesAsync() > 0;
        }
    }
}