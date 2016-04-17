using Beginor.Owin.Application.Data;
using NHibernate.AspNet.Identity;
using NHibernate;

namespace Beginor.Owin.Application.Identity {

    public class ApplicationUserStore : UserStore<ApplicationUser> {

        public ApplicationUserStore(ISession session) : base(session) { }

    }

}