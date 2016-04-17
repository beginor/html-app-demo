using System;
using NUnit.Framework;
using Microsoft.Owin.Security.DataProtection;

namespace UnitTest {

    [TestFixture]
    public class ProtectionProviderTest : WindsorTest<IDataProtectionProvider> {

        [Test]
        public void CanProtectData() {
            var dpp = Target;
            Assert.IsNotNull(dpp);
            var helloWorld = "Hello, world!";
            var dp = dpp.Create(helloWorld);
            var encrypted = dp.Protect(System.Text.Encoding.UTF8.GetBytes(helloWorld));
            Console.WriteLine(Convert.ToBase64String(encrypted));
            var clear = dp.Unprotect(encrypted);
            var result = System.Text.Encoding.UTF8.GetString(clear);

            Assert.AreEqual(helloWorld, result);
        }

    }

}

