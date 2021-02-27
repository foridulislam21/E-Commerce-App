using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Abstractions.BLL.Base;
using API.Abstractions.BLL.Core;
using API.Abstractions.Interfaces;
using API.Models;
using API.Models.OrderAggregate;

namespace API.Repositories.Services
{
    public class OrderService : IOrderService
    {
        private readonly IManager<Order> _orderRepo;
        private readonly IManager<DeliveryMethod> _deliveryRepo;
        private readonly IManager<Product> _productRepo;
        private readonly IBasketManager _basketRepo;
        public OrderService(IManager<Order> orderRepo, IManager<DeliveryMethod> deliveryRepo, IManager<Product> productRepo, IBasketManager basketRepo)
        {
            _basketRepo = basketRepo;
            _productRepo = productRepo;
            _deliveryRepo = deliveryRepo;
            _orderRepo = orderRepo;
        }

        public async Task<Order> CreateOrderAsync(string buyerEmail, int deliveryMethodId, string basketId, Address shipingAddress)
        {
            var basket = await _basketRepo.GetBasketAsync(basketId);
            var items = new List<OrderItem>();
            foreach (var item in basket.Items)
            {
                var productItem = await _productRepo.GetById(item.Id);
                var itemOrder = new ProductItemOrdered(productItem.Id, productItem.Name, productItem.PictureUrl);
                var orderItem = new OrderItem(itemOrder, productItem.Price, item.Quantity);
                items.Add(orderItem);
            }
            var deliveryMethod = await _deliveryRepo.GetById(deliveryMethodId);
            var subTotal = items.Sum(item => item.Price * item.Quantity);
            var order = new Order(items, buyerEmail, shipingAddress, deliveryMethod, subTotal);
            return order;
        }

        public Task<IReadOnlyList<DeliveryMethod>> GetDeliveryMethodsAsync()
        {
            throw new System.NotImplementedException();
        }

        public Task<Order> GetOrderByIdAsync(int id, string buyerEmail)
        {
            throw new System.NotImplementedException();
        }

        public Task<IReadOnlyList<Order>> GetOrdersForUserAsync(string buyerEmail)
        {
            throw new System.NotImplementedException();
        }
    }
}