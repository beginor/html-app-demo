using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;
using Microsoft.AspNet.Identity.Owin;
using Beginor.Owin.Application.Models;
using Beginor.Owin.Application.Data;
using Microsoft.AspNet.Identity;
using System.Collections.Generic;
using System.Security.Claims;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Beginor.Owin.Application.Identity;

namespace Beginor.Owin.Application.Controllers {

    [RoutePrefix("api/account"), Authorize]
    public class AccountController : ApiController {

        private UserManager<ApplicationUser> userManager;
        private ApplicationSignInManager signInManager;
        private IAuthenticationManager authenticationManager;

        public AccountController(UserManager<ApplicationUser> userManager, ApplicationSignInManager signInManager, IAuthenticationManager authenticationManager) {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.authenticationManager = authenticationManager;
        }

        protected override void Dispose(bool disposing) {
            if (disposing) {
                userManager.Dispose();
                signInManager.Dispose();
                //authenticationManager.Dispose();
            }
            base.Dispose(disposing);
        }

        [Route("userinfo"), HttpGet]
        public UserInfoViewModel GetUserInfo() {
            var externalLogin = ExternalLoginData.FromIdentity(User.Identity as ClaimsIdentity);
            var userInfo = new UserInfoViewModel {
                UserName = User.Identity.GetUserName(),
                HasRegistered = externalLogin == null,
                LoginProvider = externalLogin?.LoginProvider
            };
            return userInfo;
        }

        [Route("register"), HttpPost, AllowAnonymous]
        public async Task<IHttpActionResult> Register([FromBody]RegisterBindingModel model) {
            if (!ModelState.IsValid) {
                return new InvalidModelStateResult(ModelState, this);
            }

            var user = new ApplicationUser {
                Email = model.Email,
                UserName = model.Email
            };
            var identityResult = await userManager.CreateAsync(user);

            IHttpActionResult result;

            if (identityResult.Succeeded) {
                result = Ok();
            }
            else {
                var messageBuilder = new StringBuilder();
                foreach (var error in identityResult.Errors) {
                    messageBuilder.AppendLine(error);
                }
                result = BadRequest(messageBuilder.ToString());
            }
            return result;
        }

        [Route("login"), HttpPost, AllowAnonymous]
        public async Task<IHttpActionResult> Login([FromBody]LoginBindingModel model) {
            if (!ModelState.IsValid) {
                return new InvalidModelStateResult(ModelState, this);
            }

            var signInStatus = await signInManager.PasswordSignInAsync(
                model.UserName,
                model.Password,
                model.RememberMe,
                shouldLockout: true
            );

            if (signInStatus == SignInStatus.Success) {
                var userInfo = GetUserInfo();
                return Ok(userInfo);
            }
            return BadRequest("login failed, try again.");
        }

        [Route("Logout")]
        public IHttpActionResult Logout() {
            authenticationManager.SignOut(CookieAuthenticationDefaults.AuthenticationType);
            return Ok();
        }

        private class ExternalLoginData {

            public string LoginProvider { get; private set; }
            public string ProviderKey { get; private set; }

            private string UserName { get; set; }

            public IList<Claim> GetClaims() {
                IList<Claim> claims = new List<Claim>();
                claims.Add(new Claim(ClaimTypes.NameIdentifier, ProviderKey, null, LoginProvider));

                if (UserName != null) {
                    claims.Add(new Claim(ClaimTypes.Name, UserName, null, LoginProvider));
                }

                return claims;
            }

            public static ExternalLoginData FromIdentity(ClaimsIdentity identity) {
                var providerKeyClaim = identity?.FindFirst(ClaimTypes.NameIdentifier);

                if (string.IsNullOrEmpty(providerKeyClaim?.Issuer) || string.IsNullOrEmpty(providerKeyClaim.Value)) {
                    return null;
                }

                if (providerKeyClaim.Issuer == ClaimsIdentity.DefaultIssuer) {
                    return null;
                }

                return new ExternalLoginData {
                    LoginProvider = providerKeyClaim.Issuer,
                    ProviderKey = providerKeyClaim.Value,
                    UserName = identity.FindFirstValue(ClaimTypes.Name)
                };
            }
        }
    }
}