using System;
using Beginor.Owin.Application.Security;
using NUnit.Framework;
using Beginor.Owin.Application.Controllers;
using System.Web.Http.Controllers;

namespace UnitTest {

    [TestFixture]
    public class SystemRolesFinderTest : WindsorTest<ISystemRolesFinder> {

        [Test]
        public void CanFindSystemRoles() {
            var sysRoles = Target.FindSystemRoles();
            Assert.IsNotEmpty(sysRoles);

            Console.WriteLine(string.Join(", ", sysRoles));
        }

        [Test]
        public void SubTypeTest() {
            var type1 = typeof(UsersController);
            var type2 = typeof(IHttpController);
            Assert.IsTrue(type2.IsAssignableFrom(type1));
        }
    }
}

