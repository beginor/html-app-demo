using System;
using NUnit.Framework;
using Microsoft.AspNet.Identity;
using WebApi.Data;
using NHibernate.AspNet.Identity;
using System.Linq;

namespace UnitTest {

    [TestFixture]
    public class IdentityTest : WindsorTest {

        [Test]
        public void CanResolveEmailService() {
            var service = Container.Resolve<IIdentityMessageService>("emailService");
            Assert.IsNotNull(service);
        }

        [Test]
        public void CanResolveSmsService() {
            var service = Container.Resolve<IIdentityMessageService>("smsService");
            Assert.IsNotNull(service);
        }

        [Test]
        public void CanResolveUserStore() {
            var userStore = Container.Resolve<UserStore<ApplicationUser>>();
            Assert.IsNotNull(userStore);
            userStore.Users.ToList();
        }

        [Test]
        public void CanResolveRoleStore() {
            var roleStore = Container.Resolve<RoleStore<ApplicationRole>>();
            Assert.IsNotNull(roleStore);
            roleStore.Roles.ToList();
        }

        [Test]
        public void CanResolveUserManager() {
            var userManager = Container.Resolve<UserManager<ApplicationUser>>();
            Assert.IsNotNull(userManager);
        }
    }
}

