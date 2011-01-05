namespace SharpArch.Futures.Testing
{
    using System.Collections.Generic;

    using Castle.MicroKernel.Registration;

    using SharpArch.Data.NHibernate;
    using SharpArch.Data.NHibernate.FluentNHibernate;
    using SharpArch.Futures.Core.Initialization.Injection;
    using SharpArch.Futures.Core.Initialization.NHibernate;

    using NHibernateInitializer = SharpArch.Futures.Core.Initialization.NHibernate.NHibernateInitializer;

    public abstract class IntegrationTest
    {
        private readonly List<IWindsorInstaller> installers = new List<IWindsorInstaller>();

        private readonly List<NHibernateSessionConfiguration> sessions = new List<NHibernateSessionConfiguration>();

        public void AddInstaller(IWindsorInstaller installer)
        {
            this.installers.Add(installer);
        }

        public void AddSessionFor<TAutoPersistenceModelGenerator>(string sessionKey)
            where TAutoPersistenceModelGenerator : IAutoPersistenceModelGenerator, new()
        {
            var sessionDetails = new NHibernateSessionConfiguration(
                sessionKey, new SimpleSessionStorage(), new TAutoPersistenceModelGenerator());

            this.sessions.Add(sessionDetails);
        }

        public void AddSessionFor<TAutoPersistenceModelGenerator>(string sessionKey, IDictionary<string, string> nhibernateProperties)
            where TAutoPersistenceModelGenerator : IAutoPersistenceModelGenerator, new()
        {
            var sessionDetails = new NHibernateSessionConfiguration(
                sessionKey, new SimpleSessionStorage(), new TAutoPersistenceModelGenerator(), nhibernateProperties);

            this.sessions.Add(sessionDetails);
        }

        protected void Initialize()
        {
            if (this.sessions.Count > 0)
            {
                NHibernateInitializer.Initialize(this.sessions);
            }

            ServiceLocatorInitializer.Initialize(this.installers);
        }
    }
}