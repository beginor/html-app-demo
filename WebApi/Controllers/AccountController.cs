using System.Web.Http;
using System.Web.Http.Results;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using WebApi.Models;

namespace WebApi.Controllers {

    [Authorize]
    [RoutePrefix("api/account")]
    public class AccountController : ApiController {

        private ApplicationUserManager userManager;
        private ApplicationSignInManager signInManager;

        public AccountController(ApplicationUserManager userManager, ApplicationSignInManager signInManager) {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("login")]
        public IHttpActionResult Login([FromBody]LoginModel model) {
            if (!ModelState.IsValid) {
                return new InvalidModelStateResult(ModelState, this);
            }
        }
    }
}