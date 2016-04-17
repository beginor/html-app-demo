using Beginor.Owin.Application.Data;
using NHibernate.AspNet.Identity;
using NHibernate;

namespace Beginor.Owin.Application.Identity {

    public class ApplicationRoleStore : RoleStore<ApplicationRole> {

        public ApplicationRoleStore(ISession session) : base(session) { }

    }

}