using System.Threading.Tasks;
using API.Abstractions.BLL.Base;
using API.Models;

namespace API.Abstractions.BLL.Core
{
    public interface IBasketManager
    {
        Task<CustomerBasket> GetBasketAsync (string basketId);
        Task<CustomerBasket> UpdateBasketAsync (CustomerBasket basket);
        Task<bool> DeleteBasketAsync (string basketId);
    }
}