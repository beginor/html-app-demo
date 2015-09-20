using NHibernate.AspNet.Identity;
using WebApi.Models;

namespace WebApi {

    public class ApplicationUserStore : UserStore<ApplicationUser> {

        private HibernateContext dataContext;

        public ApplicationUserStore(HibernateContext dataContext) : base(dataContext.OpenSession()) {
            this.dataContext = dataContext;
        }

        protected override void Dispose(bool disposing) {
            if (disposing) {
                dataContext = null;
            }
            base.Dispose(disposing);
        }

    }

}