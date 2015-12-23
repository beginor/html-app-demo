using Microsoft.AspNet.Identity;
using System.Threading.Tasks;
using WebApi.Data;
using Microsoft.AspNet.Identity.Owin;
using System;
using Microsoft.Owin.Security.DataProtection;
using Microsoft.Owin.Security;
using NHibernate.AspNet.Identity;
using NHibernate;

namespace WebApi.IdentitySupport {

    public class EmailService : IIdentityMessageService {

        public Task SendAsync(IdentityMessage message) {
            // Plug in your email service here to send an email.
            return Task.FromResult(0);
        }

    }

    public class SmsService : IIdentityMessageService {

        public Task SendAsync(IdentityMessage message) {
            // Plug in your SMS service here to send a text message.
            return Task.FromResult(0);
        }
    }

    public class ApplicationUserStore : UserStore<ApplicationUser> {

        public ApplicationUserStore(ISession session) : base(session) { }

    }

    public class ApplicationRoleStore : RoleStore<ApplicationRole> {

        public ApplicationRoleStore(ISession session) : base(session) { }

    }

    public class ApplicationUserManager : UserManager<ApplicationUser> {

        public ApplicationUserManager(
            UserStore<ApplicationUser> store,
            IIdentityMessageService emailService,
            IIdentityMessageService smsService,
            IDataProtectionProvider dataProtectionProvider
        ) : base(store) {
            // Configure validation logic for usernames
            UserValidator = new UserValidator<ApplicationUser>(this) {
                AllowOnlyAlphanumericUserNames = false,
                RequireUniqueEmail = true
            };
            // Configure validation logic for passwords
            PasswordValidator = new PasswordValidator {
                RequiredLength = 6,
                RequireNonLetterOrDigit = false,
                RequireDigit = false,
                RequireLowercase = false,
                RequireUppercase = false
            };
            // Configure user lockout defaults
            UserLockoutEnabledByDefault = true;
            DefaultAccountLockoutTimeSpan = TimeSpan.FromMinutes(5);
            MaxFailedAccessAttemptsBeforeLockout = 5;
            // Register two factor authentication providers. This application uses Phone and Emails as a step of receiving a code for verifying the user
            // You can write your own provider and plug it in here.
            RegisterTwoFactorProvider("Phone Code", new PhoneNumberTokenProvider<ApplicationUser> {
                MessageFormat = "Your security code is {0}"
            });
            RegisterTwoFactorProvider("Email Code", new EmailTokenProvider<ApplicationUser> {
                Subject = "Security Code",
                BodyFormat = "Your security code is {0}"
            });
            EmailService = emailService;
            SmsService = smsService;
            UserTokenProvider = new DataProtectorTokenProvider<ApplicationUser>(dataProtectionProvider.Create("ASP.NET Identity"));
        }

    }

    public class ApplicationSignInManager : SignInManager<ApplicationUser, string> {

        public ApplicationSignInManager(UserManager<ApplicationUser> userManager, IAuthenticationManager authenticationManager) : base(userManager, authenticationManager) {
        }

    }

}