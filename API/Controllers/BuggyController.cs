using API.Configurations.Error;
using API.StorageCenter;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers {
    public class BuggyController : BaseApiController {
        private readonly StoreContext _store;
        public BuggyController (StoreContext store) {
            _store = store;
        }

        [HttpGet ("notfound")]
        public ActionResult GetNotFoundRequest () {
            var thing = _store.Products.Find (42);
            if (thing == null) {
                return NotFound (new ApiResponse (404));
            }
            return Ok ();
        }

        [HttpGet ("servererror")]
        public ActionResult GetServerError () {
            var thing = _store.Products.Find (42);
            var thingToReturn = thing.ToString ();
            return Ok ();
        }

        [HttpGet ("badrequest")]
        public ActionResult GetBadRequest () {
            return BadRequest (new ApiResponse (400));
        }

        [HttpGet ("badrequest/{id}")]
        public ActionResult GetNotFoundRequest (int id) {
            return Ok ();
        }
    }
}