using System;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;

namespace Beginor.Owin.Application.Controllers {

    /// <summary>
    /// Controllers Installer, register all controllers to windsor container;
    /// </summary>
    public class Installer : IWindsorInstaller {

        public void Install(IWindsorContainer container, IConfigurationStore store) {
            container.Register(
                Classes.FromThisAssembly()
                       .InSameNamespaceAs<Installer>()
                       .Configure(cfg => cfg.LifestyleTransient())
            );
        }
    }

}

