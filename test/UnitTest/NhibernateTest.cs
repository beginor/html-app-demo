using System;
using System.IO;
using System.Linq;
using NHibernate.Cfg;
using NHibernate.Linq;
using NHibernate.Tool.hbm2ddl;
using NUnit.Framework;
using Beginor.Owin.Application.Data;

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

    }
}

