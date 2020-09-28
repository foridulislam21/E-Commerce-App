using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Abstractions.Repository.Base;
using API.Abstractions.Specification;
using API.Models;
using API.Repositories.Specification;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories.Base {
    public class EFRepository<T> : IRepository<T> where T : BaseEntity {
        private readonly DbContext _db;
        public EFRepository (DbContext db) {
            _db = db;
        }
        public virtual async Task<bool> Add (T entity) {
            await _db.Set<T> ().AddAsync (entity);
            return await _db.SaveChangesAsync () > 0;
        }

        public virtual async Task<IReadOnlyList<T>> GetAll () {
            return await _db.Set<T> ().ToListAsync ();
        }

        public virtual async Task<T> GetById (int id) {
            return await _db.Set<T> ().FindAsync (id);
        }

        public virtual async Task<bool> Remove (T entity) {
            _db.Set<T> ().Remove (entity);
            return await _db.SaveChangesAsync () > 0;
        }

        public virtual async Task<bool> Update (T entity) {
            _db.Set<T> ().Update (entity);
            return await _db.SaveChangesAsync () > 0;
        }
        public async Task<T> GetEntityWithSpec (ISpecification<T> spec) {
            return await ApplySpecification(spec).FirstOrDefaultAsync();
        }

        public async Task<IReadOnlyList<T>> ListAsync (ISpecification<T> spec) {
            return await ApplySpecification(spec).ToListAsync();
        }
        private IQueryable<T> ApplySpecification (ISpecification<T> spec) {
            return SpecifationEvaluator<T>.GetQuery (_db.Set<T> ().AsQueryable (), spec);
        }

    }
}