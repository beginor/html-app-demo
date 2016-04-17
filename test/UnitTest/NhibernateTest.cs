using NUnit.Framework;
using System;
using System.IO;
using NHibernate.Cfg;
using Beginor.Owin.Application.Data;
using NHibernate.Linq;
using System.Linq;
using Microsoft.Owin.Security.DataProtection;
using NHibernate.Tool.hbm2ddl;

namespace UnitTest {

    [TestFixture]
    public class NhibernateTest : WindsorTest {
        
        [Test]
        public void CanBuildSessionFactory() {
            var cfg = new Configuration();
            var configFile = "hibernate.config";
            if (!Path.IsPathRooted(configFile)) {
                configFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, configFile);
            }
            cfg.Configure(configFile);

            using (var sessionFactory = cfg.BuildSessionFactory()) {
                
                var session = sessionFactory.OpenSession();

                var users = session.Query<ApplicationUser>().ToList();

                Console.WriteLine(users.Count);
            }
        }

        [Test]
        public void CanGenerateSchema() {
            var cfg = new Configuration();
            var configFile = "hibernate.config";
            if (!Path.IsPathRooted(configFile)) {
                configFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, configFile);
            }
            cfg.Configure(configFile);
            var export = new SchemaExport(cfg);
            export.Drop(true, true);
            export.Execute(true, true, false);
        }

        [Test]
        public void CanUpdateSchema() {
            var cfg = new Configuration();
            var configFile = "hibernate.config";
            if (!Path.IsPathRooted(configFile)) {
                configFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, configFile);
            }
            cfg.Configure(configFile);
            //var export = new SchemaExport(cfg);
            //export.Execute(true, true, true);

            var update = new SchemaUpdate(cfg);
            update.Execute(true, true);
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

