using System;
using NUnit.Framework;
using Beginor.Owin.Application.Controllers;

namespace UnitTest {

    [TestFixture]
    public class RolesControllerTest : WindsorTest<RolesController> {

        [Test]
        public void CanRefreshRoles() {
            var result = Target.Refresh();
            Assert.IsNotNull(result);
        }

    }
}

