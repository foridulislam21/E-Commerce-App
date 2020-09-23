using API.Configurations.Error;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers {
    [Route ("errors/{code}")]
    public class ErrorController : BaseApiController {
        public IActionResult Error (int code) {
            return new ObjectResult (new ApiResponse (404));
        }
    }
}