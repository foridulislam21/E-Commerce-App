using System.Collections.Generic;
using System.Threading.Tasks;
using API.Abstractions.Specification;
using API.Models;

namespace API.Abstractions.Repository.Base
{
    public interface IRepository<T> where T : BaseEntity
    {

        Task<bool> Add (T entity);
        Task<bool> Remove (T entity);
        Task<bool> Update (T entity);
        Task<IReadOnlyList<T>> GetAll ();
        Task<T> GetById (int id);
        Task<T> GetEntityWithSpec (ISpecification<T> spec);
        Task<IReadOnlyList<T>> ListAsync (ISpecification<T> spec);
        Task<int> CountAsync (ISpecification<T> spec);

    }
}