namespace SharpArch.Futures.Web
{
    using System;
    using System.Collections.Generic;
    using System.Web;

    using Castle.Windsor;

    using SharpArch.Data.NHibernate;
    using SharpArch.Data.NHibernate.FluentNHibernate;
    using SharpArch.Futures.Core.Initialization.Injection;
    using SharpArch.Futures.Core.Initialization.NHibernate;

    using NHibernateInitializer = SharpArch.Futures.Core.Initialization.NHibernate.NHibernateInitializer;

    public abstract class SharpArchApplication : HttpApplication
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
                sessionKey, this.GetNewSessionStorage(), new TAutoPersistenceModelGenerator());

            this.sessions.Add(sessionDetails);
        }

        public void AddSessionFor<TAutoPersistenceModelGenerator>(string sessionKey, IDictionary<string, string> nhibernateProperties)
            where TAutoPersistenceModelGenerator : IAutoPersistenceModelGenerator, new()
        {
            var sessionDetails = new NHibernateSessionConfiguration(
                sessionKey, this.GetNewSessionStorage(), new TAutoPersistenceModelGenerator(), nhibernateProperties);

            this.sessions.Add(sessionDetails);
        }

        protected virtual void Application_BeginRequest(object sender, EventArgs e)
        {
            NHibernateInitializer.Initialize(this.sessions);
        }

        protected virtual void Application_Start(object sender, EventArgs e)
        {
            ServiceLocatorInitializer.Initialize(this.installers);
        }

        protected abstract ISessionStorage GetNewSessionStorage();
    }
}