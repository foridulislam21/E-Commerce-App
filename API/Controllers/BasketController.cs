using System.Threading.Tasks;
using API.Abstractions.BLL.Core;
using API.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class BasketController : BaseApiController
    {
        private readonly IBasketManager _basketManager;
        public BasketController (IBasketManager basketManager)
        {
            _basketManager = basketManager;
        }

        [HttpGet]
        public async Task<ActionResult<CustomerBasket>> GetBasketById (string id)
        {
            var basket = await _basketManager.GetBasketAsync (id);
            return Ok (basket?? new CustomerBasket (id));
        }

        [HttpPost]
        public async Task<ActionResult<CustomerBasket>> UpdateBasket (CustomerBasket basket)
        {
            var updatedBasket = await _basketManager.UpdateBasketAsync (basket);
            return Ok (updatedBasket);
        }

        [HttpDelete]
        public async Task DeleteBasketAsync (string id)
        {
            await _basketManager.DeleteBasketAsync (id);
        }
    }
}