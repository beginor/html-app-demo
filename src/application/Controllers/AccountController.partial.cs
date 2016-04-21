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
    
    partial class AccountController {

        [HttpGet, Route("userinfo")]
        public IHttpActionResult GetUserInfo() {
            var identity = User?.Identity as ClaimsIdentity;
            var provider = identity?.FindFirst(ClaimTypes.NameIdentifier);

            var userInfo = new UserInfoModel {
                Name = identity?.FindFirstValue(ClaimTypes.Name),
                Authorized = identity != null,
                //Roles = identity?.FindAll(ClaimTypes.Role).Select(c => c.Value).ToArray(),
                LoginProvider = provider?.Issuer
            };
            userInfo.Roles = new Dictionary<string, bool>();
            if (identity != null) {
                var userRoles = identity.FindAll(ClaimTypes.Role).Select(c => c.Value);
                foreach (var role in userRoles) {
                    userInfo.Roles[role] = true;
                }
            }

            if (identity != null) {
                var appUser = this.userMgr.FindById(identity.GetUserId());
                //userInfo.Area = appUser.Area;
            }
            return Ok(userInfo);
        }

    }
}

