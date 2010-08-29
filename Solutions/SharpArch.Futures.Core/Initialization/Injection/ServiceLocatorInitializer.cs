namespace SharpArch.Futures.Core.Initialization.Injection
{
    using System.Collections.Generic;
    using System.Linq;

    using Castle.Windsor;

    using CommonServiceLocator.WindsorAdapter;

    using Microsoft.Practices.ServiceLocation;

    public static class ServiceLocatorInitializer
    {
        public static void Initialize(IEnumerable<IWindsorInstaller> installers)
        {
            IWindsorContainer container = new WindsorContainer();
            container.Install(installers.ToArray());

            ServiceLocator.SetLocatorProvider(() => new WindsorServiceLocator(container));
        }
    }
}