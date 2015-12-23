using System;
using Castle.MicroKernel.Facilities;
using NHibernate.Cfg;
using Castle.MicroKernel.Registration;
using NHibernate;
using System.IO;

namespace WebApi {

    public class NHibernateFacility : AbstractFacility {
        
        protected override void Init() {
            var configFile = FacilityConfig.Attributes["configFile"];
            if (string.IsNullOrEmpty(configFile)) {
                throw new FacilityException("configFile is null or empty!");
            }
            else {
                RegisterSessionFactory(configFile);
                RegisterSession();
            }
        }

        private void RegisterSessionFactory(string configFile) {
            if (!File.Exists(configFile)) {
                throw new FacilityException("Config file not exists.");
            }
            Kernel.Register(
                Component.For<ISessionFactory>()
                .UsingFactoryMethod(() => {
                    var cfg = new Configuration();
                    cfg.Configure(configFile);
                    return cfg.BuildSessionFactory();
                }).LifestyleSingleton()
            );
        }

        private void RegisterSession() {
            Kernel.Register(
                Component.For<ISession>().UsingFactoryMethod((kernel, context) => {
                    var sessionFactory = kernel.Resolve<ISessionFactory>();
                    return sessionFactory.OpenSession();
                }).LifestyleTransient()
            );
        }
    }
}

