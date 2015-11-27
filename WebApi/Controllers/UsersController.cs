using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Castle.Core.Logging;
using NHibernate.Linq;
using WebApi.Models;
using NHibernate;
using WebApi.Data;

namespace WebApi.Controllers {

    public class UsersController : ApiController {

        private ISessionFactory dataContext;

        public ILogger Logger { get; set; } = NullLogger.Instance;

        public UsersController(ISessionFactory dataContext) {
            this.dataContext = dataContext;
        }

        protected override void Dispose(bool disposing) {
            if (disposing) {
                dataContext = null;
            }
            base.Dispose(disposing);
        }

        public IHttpActionResult GetAll() {
            IHttpActionResult result;
            using (var session = dataContext.OpenSession()) {
                try {
                    var query = session.Query<ApplicationUser>();
                    var data = query.ToList();
                    result = Json(data);
                }
                catch (Exception ex) {
                    result = InternalServerError(ex);
                    Logger.Error("Can not get all users.", ex);
                }
            }
            return result;
        }

    }

}