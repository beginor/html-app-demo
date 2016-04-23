using System.Linq;
using Microsoft.AspNet.Identity;
using NHibernate.AspNet.Identity;
using NUnit.Framework;
using Beginor.Owin.Application.Data;
using Beginor.Owin.Application.Security;
using Beginor.Owin.Application.Controllers;
using System;
using NHibernate;
using NHibernate.Linq;
using Beginor.Owin.Application.Identity;
using Microsoft.Owin.Security.DataProtection;

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

        [Test]
        public void CanCreateAdminUser() {
            var rolesCtrl = Container.Resolve<RolesController>();
            rolesCtrl.Refresh();
            rolesCtrl.Dispose();

            var session = Container.Resolve<ISession>();
            using (session) {
                // roles
                var rolesStore = new ApplicationRoleStore(session);
                var roles = rolesStore.Roles.ToList();
                var userName = "admin";
                var userEmail = "admin@localhost.com";
                var userPass = "admin888";
                var userStore = new ApplicationUserStore(session);
                var dataProtProvider = Container.Resolve<IDataProtectionProvider>();
                var userManager = new ApplicationUserManager(userStore, null, null, dataProtProvider);

                var user = userManager.FindByName(userName);
                if (user == null) {
                    user = new ApplicationUser {
                        UserName = userName,
                        Email = userEmail,
                        LockoutEnabled = false
                    };
                    var createResult = userManager.Create(user);
                    Assert.IsTrue(createResult.Succeeded);
                    Assert.IsNotEmpty(user.Id);
                    var addPassResult = userManager.AddPassword(user.Id, userPass);
                    Assert.IsTrue(addPassResult.Succeeded);
                }
                else {
                    if (!userManager.CheckPassword(user, userPass)) {
                        var token = userManager.GeneratePasswordResetToken(user.Id);
                        var resetPassResult = userManager.ResetPassword(user.Id, token, userPass);
                        Assert.IsTrue(resetPassResult.Succeeded);
                    }
                }
                foreach (var r in roles) {
                    var role = session.Query<ApplicationRole>().FirstOrDefault(x => x.Name == r.Name);
                    if (role == null) {
                        role = new ApplicationRole {
                            Name = r.Name,
                            Description = r.Description,
                            Module = r.Module
                        };
                        session.Save(role);
                        user.Roles.Add(role);
                        role.Users.Add(user);
                        session.Update(user);
                        session.Update(role);
                    }
                    else {
                        var isInRole = user.Roles.Any(x => x.Name == r.Name);
                        if (!isInRole) {
                            user.Roles.Add(role);
                            role.Users.Add(user);
                            session.Update(user);
                            session.Update(role);
                        }
                    }
                }
                session.Flush();
            }
        }

    }
}

