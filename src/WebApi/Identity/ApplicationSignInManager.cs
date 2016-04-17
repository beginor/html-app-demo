using Microsoft.AspNet.Identity;
using Beginor.Owin.Application.Data;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;

namespace Beginor.Owin.Application.Identity {

    public class ApplicationSignInManager : SignInManager<ApplicationUser, string> {

        public ApplicationSignInManager(
            UserManager<ApplicationUser> userManager,
            IAuthenticationManager authenticationManager
        ) : base(userManager, authenticationManager) {
        }

    }

}