using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using OAuth_Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web.Http;

namespace OAuth_Project.Controllers
{
    public class AccountController : ApiController
    {
        [Route("api/User/Register")]
        [HttpPost]
        [AllowAnonymous]
        public IdentityResult Register(AccountModel account)
        {
            var usertore = new UserStore<ApplicationUser>(new ApplicationDbContext());
            var userManager = new UserManager<ApplicationUser>(usertore);
            var user = new ApplicationUser()
            {
                UserName = account.UserName,
                Email = account.Email,
                FirstName = account.FirstName,
                LastName = account.LastName
            };

            userManager.PasswordValidator = new PasswordValidator() { RequiredLength = 3 };

            IdentityResult result = userManager.Create(user, account.Password);

            userManager.AddToRoles(user.Id , account.Roles);

            return result;
        }
        
        [HttpGet]
        [Route("api/GetUserClaims")]
        public AccountModel GetUserClaims(AccountModel account)
        {
            var identityClaim = (ClaimsIdentity)User.Identity;

            AccountModel model = new AccountModel()
            {
                UserName = identityClaim.FindFirst("UserName").Value,
                Email = identityClaim.FindFirst("Email").Value,
                FirstName = identityClaim.FindFirst("FirstName").Value,
                LastName = identityClaim.FindFirst("LastName").Value,
                LoggedOn = identityClaim.FindFirst("LoggedOn").Value,
            };

            return model;
        }


        [HttpGet]
        [Authorize(Roles = "Admin")]
        [Route("api/ForAdminRole")]
        public string GetForAdminRole()
        {
            return "Message from Admin Role";
        }
        
        [HttpGet]
        [Authorize(Roles = "Author")]
        [Route("api/ForAuthorRole")]
        public string GetForAuthorRole()
        {
            return "Message from Author Role";
        }


        [HttpGet]
        [Authorize(Roles = "Author,Reader")]
        [Route("api/ForAuthorOrReaderRole")]
        public string GetForAuthorOrReaderRole()
        {
            return "Message from Author or Reader Role";
        }

    }
}
