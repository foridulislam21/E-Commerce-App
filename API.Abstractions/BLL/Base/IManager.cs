using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Abstractions.BLL.Base
{
    public interface IManager<T> where T : class
    {
        Task<bool> Add(T entity);
        Task<bool> Remove(T entity);
        Task<bool> Update(T entity);
        Task<IReadOnlyList<T>> GetAll();
        Task<T> GetById(int id);
    }
}