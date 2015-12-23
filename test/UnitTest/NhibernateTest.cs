using NUnit.Framework;
using System;
using NHibernate;
using NHibernate.Cfg;
using WebApi.Data;
using NHibernate.Linq;
using System.Linq;
using Microsoft.Owin.Security.DataProtection;
using Microsoft.AspNet.Identity;

namespace UnitTest {

    [TestFixture]
    public class NhibernateTest : WindsorTest {
        
        [Test]
        public void CanBuildSessionFactory() {
            var cfg = new Configuration();
            cfg.Configure("hibernate.config");
            using (var sessionFactory = cfg.BuildSessionFactory()) {
                var session = sessionFactory.OpenSession();

                var users = session.Query<ApplicationUser>().ToList();

                Console.WriteLine(users.Count);
            }
        }

        [Test]
        public void CanResolveDataProtectionProvider() {
            var dpp = Container.Resolve<IDataProtectionProvider>();
            Assert.IsNotNull(dpp);
            var helloWorld = "Hello, world!";
            var dp = dpp.Create(helloWorld);
            var encryptd = dp.Protect(System.Text.Encoding.UTF8.GetBytes(helloWorld));
            var clear = dp.Unprotect(encryptd);
            var result = System.Text.Encoding.UTF8.GetString(clear);

            Assert.AreEqual(helloWorld, result);
        }

    }
}

