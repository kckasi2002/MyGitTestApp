using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using OAuth_Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace OAuth_Project.Controllers
{
    public class RoleController : ApiController
    {
        [Route("api/GetAllRoles")]
        [HttpGet]
        [AllowAnonymous]
        public HttpResponseMessage GetRoles()
        {
            var roleStore = new RoleStore<IdentityRole>(new ApplicationDbContext());
            var roleManager = new RoleManager<IdentityRole>(roleStore);

            var roles = roleManager.Roles.Select(a => new { a.Id , a.Name})
                                        .ToList();

            return this.Request.CreateResponse(HttpStatusCode.OK, roles);
        }

        [Route("api/GetAllRoles")]
        [HttpGet]
        [AllowAnonymous]
        public int GetUnitTestPurpose()
        {
            return 1;
        }
    }
}
