using System.Collections.Generic;
using System.Threading.Tasks;
using API.Models.OrderAggregate;

namespace API.Abstractions.Interfaces
{
    public interface IOrderService
    {
        Task<Order> CreateOrderAsync (string buyerEmail, int deliveryMethod, string basketId, Address shipingAddress);
        Task<IReadOnlyList<Order>> GetOrdersForUserAsync (string buyerEmail);
        Task<Order> GetOrderByIdAsync (int id, string buyerEmail);
        Task<IReadOnlyList<DeliveryMethod>> GetDeliveryMethodsAsync ();
    }
}