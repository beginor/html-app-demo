using System.Security.Claims;
using System.Threading.Tasks;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using WebApi.Models;

namespace WebApi {

    public class ApplicationSignInManager : SignInManager<ApplicationUser, string> {

        private static bool registerToWindsor;

        public ApplicationSignInManager(UserManager<ApplicationUser> userManager, IAuthenticationManager authenticationManager)
            : base(userManager, authenticationManager) {
        }

        public override Task<ClaimsIdentity> CreateUserIdentityAsync(ApplicationUser user) {
            return user.GenerateUserIdentityAsync((ApplicationUserManager)UserManager);
        }

        public static ApplicationSignInManager Create(IdentityFactoryOptions<ApplicationSignInManager> options, IOwinContext context) {
            var signinManager = new ApplicationSignInManager(context.GetUserManager<ApplicationUserManager>(), context.Authentication);
            var container = context.Get<IWindsorContainer>();

            if (!registerToWindsor) {
                container.Register(
                        Component.For<ApplicationSignInManager>().UsingFactoryMethod(() => signinManager)
                    );
                registerToWindsor = true;
            }
            return signinManager;
        }

    }

}