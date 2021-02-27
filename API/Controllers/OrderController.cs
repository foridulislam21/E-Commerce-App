using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using API.Abstractions.Interfaces;
using API.Configurations.Error;
using API.Configurations.Extentions;
using API.Models.DTO;
using API.Models.OrderAggregate;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Authorize]
    public class OrderController : BaseApiController
    {
        private readonly IOrderService _orderService;
        private readonly IMapper _mapper;
        public OrderController(IOrderService orderService, IMapper mapper)
        {
            _mapper = mapper;
            _orderService = orderService;
        }
        [HttpPost]
        public async Task<ActionResult<Order>> CreateOrder(OrderDto orderDto)
        {
            var email = HttpContext.User.RetrieveEmailFromPrincipal();
            var address = _mapper.Map<AddressDto, Address>(orderDto.ShipToAddress);
            var order = await _orderService.CreateOrderAsync(email, orderDto.DeliveryMethodId, orderDto.BasketId, address);
            if (order == null)
            {
                return BadRequest(new ApiResponse(400, "Problem with creating order."));
            }
            return Ok(order);
        }
    }
}