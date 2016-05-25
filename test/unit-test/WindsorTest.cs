using NUnit.Framework;
using Castle.Windsor;
using Castle.Windsor.Installer;
using Castle.MicroKernel.Registration;

namespace UnitTest {

    public abstract class WindsorTest {

        protected IWindsorContainer Container { get; private set; }

        [SetUp]
        public virtual void Setup() {
            Container = new WindsorContainer();
            Container.Install(
                Configuration.FromXmlFile("windsor.config"),
                FromAssembly.Named("Beginor.Owin.Application")
            );

            Container.Register(
                Component.For<IWindsorContainer>().Instance(Container)
            );
        }
    }

    public abstract class WindsorTest<TTarget> : WindsorTest {

        protected TTarget Target { get; private set; }

        public override void Setup() {
            base.Setup();
            Target = Container.Resolve<TTarget>();
        }

    }
}

