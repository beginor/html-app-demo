using System;
using Microsoft.Owin.Security.Cookies;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity;
using Beginor.Owin.Application.Data;

namespace Beginor.Owin.Application.Security {
    
    public class ApplicationCookieAuthenticationcProvider : CookieAuthenticationProvider {

        public ApplicationCookieAuthenticationcProvider() {
            this.OnValidateIdentity = SecurityStampValidator.OnValidateIdentity<UserManager<ApplicationUser>, ApplicationUser>(
                validateInterval: TimeSpan.FromMinutes(30),
                regenerateIdentity: (manager, user) => manager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie)
            );
        }

    }
}

