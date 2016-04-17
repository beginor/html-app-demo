using Microsoft.AspNet.Identity;
using Beginor.Owin.Application.Data;
using Microsoft.AspNet.Identity.Owin;
using System;
using Microsoft.Owin.Security.DataProtection;
using NHibernate.AspNet.Identity;

namespace Beginor.Owin.Application.Identity {

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

}