using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Abstractions.Repository.Base
{
    public interface IRepository<T> where T : class
    {

        Task<bool> Add(T entity);
        Task<bool> Remove(T entity);
        Task<bool> Update(T entity);
        Task<ICollection<T>> GetAll();
        Task<T> GetById(int id);
    }
}