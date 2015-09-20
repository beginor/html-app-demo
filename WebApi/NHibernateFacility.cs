using System;
using Castle.MicroKernel.Facilities;
using Castle.MicroKernel.Registration;

namespace WebApi {

    public class NHibernateFacility : AbstractFacility {

        protected override void Init() {
            Console.WriteLine("Hello, windsor.");
        }

        protected override void Dispose() {
            base.Dispose();
        }

    }

}