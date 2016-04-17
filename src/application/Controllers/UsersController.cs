using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Castle.Core.Logging;
using NHibernate.Linq;
using Beginor.Owin.Application.Models;
using NHibernate;
using Beginor.Owin.Application.Data;
using Microsoft.AspNet.Identity;

namespace Beginor.Owin.Application.Controllers {

    [RoutePrefix("api/users")]
    public class UsersController : ApiController {

        private readonly UserManager<ApplicationUser> userManager;

        public ILogger Logger { get; set; } = NullLogger.Instance;

        public UsersController(UserManager<ApplicationUser> userManager) {
            this.userManager = userManager;
        }

        protected override void Dispose(bool disposing) {
            if (disposing) {
                userManager.Dispose();
            }
            base.Dispose(disposing);
        }

        [Route("")]
        public IHttpActionResult GetAll() {
            IHttpActionResult result;
            try {
                var query = userManager.Users;
                var data = query.ToList();
                result = Ok(data);
            }
            catch (Exception ex) {
                result = InternalServerError(ex);
                Logger.Error("Can not get all users.", ex);
            }
            return result;
        }

        public IHttpActionResult Post(ApplicationUser user) {
            return Ok();
        }

    }

}