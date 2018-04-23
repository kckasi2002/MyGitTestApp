using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;
using OAuth_Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;

namespace OAuth_Project
{
    public class ApplicationOAuthProvider : OAuthAuthorizationServerProvider
    {
        //public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        //{
        //    context.Validated();

        //    return;
        //}

        public override Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();

            return base.ValidateClientAuthentication(context);
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            var userStore = new UserStore<ApplicationUser>(new ApplicationDbContext());
            var userManager = new UserManager<ApplicationUser>(userStore);

            var user = await userManager.FindAsync(context.UserName, context.Password);

            if(user != null)
            {
                var identity = new ClaimsIdentity(context.Options.AuthenticationType);
                identity.AddClaims(new List<Claim>() {
                    new Claim("UserName" , user.UserName),
                    new Claim("Email" , user.Email),
                    new Claim("FirstName" , user.FirstName),
                    new Claim("LastName" , user.LastName),
                    new Claim("LoggedOn" , DateTime.Now.ToString())
                });

                var userRoles = userManager.GetRoles(user.Id);
                foreach (var roleName in userRoles)
                {
                    identity.AddClaim(new Claim(ClaimTypes.Role, roleName));
                }

                var additionalData = new AuthenticationProperties(new Dictionary<string, string>() {
                    {
                        "role", Newtonsoft.Json.JsonConvert.SerializeObject(userRoles)
                    }
                });

                var token = new AuthenticationTicket(identity, additionalData);

                context.Validated(token);
            }
            else
            {
                return;
            }

        }


        public override Task TokenEndpoint(OAuthTokenEndpointContext context)
        {

            foreach (KeyValuePair<string,string> property in context.Properties.Dictionary)
            {
                context.AdditionalResponseParameters.Add(property.Key, property.Value);
            }
            return Task.FromResult<Object>(null);
        }


    }
}