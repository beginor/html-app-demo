using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OAuth;
using Microsoft.AspNet.Identity;
using Beginor.Owin.Application.Data;

namespace Beginor.Owin.Application.OAuth {

    public class ApplicationOAuthProvider : OAuthAuthorizationServerProvider {

        private readonly string publicClientId;
        private readonly UserManager<ApplicationUser> userManager;

        public ApplicationOAuthProvider(string publicClientId, UserManager<ApplicationUser> userManager) {
            if (publicClientId == null) {
                throw new ArgumentNullException(nameof(publicClientId));
            }
            if (userManager == null) {
                throw new ArgumentNullException(nameof(userManager));
            }

            this.publicClientId = publicClientId;
            this.userManager = userManager;
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context) {
            var user = await userManager.FindAsync(context.UserName, context.Password);

            if (user == null) {
                context.SetError("invalid_grant", "The user name or password is incorrect.");
                return;
            }


            var oAuthIdentity = await userManager.CreateIdentityAsync(user, OAuthDefaults.AuthenticationType);
            var cookiesIdentity = await userManager.CreateIdentityAsync(user, CookieAuthenticationDefaults.AuthenticationType);

            var properties = CreateProperties(user.UserName);
            var ticket = new AuthenticationTicket(oAuthIdentity, properties);
            context.Validated(ticket);
            context.Request.Context.Authentication.SignIn(cookiesIdentity);
        }

        public override Task TokenEndpoint(OAuthTokenEndpointContext context) {
            foreach (var property in context.Properties.Dictionary) {
                context.AdditionalResponseParameters.Add(property.Key, property.Value);
            }

            return Task.FromResult<object>(null);
        }

        public override Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context) {
            // Resource owner password credentials does not provide a client ID.
            if (context.ClientId == null) {
                context.Validated();
            }

            return Task.FromResult<object>(null);
        }

        public override Task ValidateClientRedirectUri(OAuthValidateClientRedirectUriContext context) {
            if (context.ClientId == publicClientId) {
                Uri expectedRootUri = new Uri(context.Request.Uri, "/");

                if (expectedRootUri.AbsoluteUri == context.RedirectUri) {
                    context.Validated();
                }
            }

            return Task.FromResult<object>(null);
        }

        public static AuthenticationProperties CreateProperties(string userName) {
            IDictionary<string, string> data = new Dictionary<string, string> {
                { "userName", userName }
            };
            return new AuthenticationProperties(data);
        }
    }

}

