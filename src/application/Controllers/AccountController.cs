using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Beginor.Owin.Application.Identity;
using Beginor.Owin.Application.Data;
using Beginor.Owin.Application.Models;

namespace Beginor.Owin.Application.Controllers {

    [RoutePrefix("api/account")]
    public partial class AccountController : ApiController {

        private ApplicationSignInManager signInManager;
        private IAuthenticationManager authenticationManager;
        private UserManager<ApplicationUser> userMgr;

        public AccountController(ApplicationSignInManager signInManager, IAuthenticationManager authenticationManager, UserManager<ApplicationUser> userMgr) {
            this.signInManager = signInManager;
            this.authenticationManager = authenticationManager;
            this.userMgr = userMgr;
        }

        protected override void Dispose(bool disposing) {
            if (disposing) {
                this.signInManager = null;
                this.authenticationManager = null;
                this.userMgr = null;
            }
            base.Dispose(disposing);
        }

        [HttpPost, Route("login")]
        public async Task<IHttpActionResult> Login([FromBody]LoginModel model) {
            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }
            var status = await signInManager.PasswordSignInAsync(
                model.UserName,
                model.Password,
                model.RememberMe,
                shouldLockout: true
            );
            if (status == SignInStatus.Success) {
                return Ok();
            }
            return BadRequest(status.ToString());
        }

        [HttpPost, Route("logout")]
        public IHttpActionResult Logout() {
            authenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return Ok();
        }

    }

}