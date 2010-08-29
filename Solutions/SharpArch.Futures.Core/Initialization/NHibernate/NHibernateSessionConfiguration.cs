namespace SharpArch.Futures.Core.Initialization.NHibernate
{
    using System.Collections.Generic;

    using SharpArch.Data.NHibernate;
    using SharpArch.Data.NHibernate.FluentNHibernate;

    public class NHibernateSessionConfiguration
    {
        private readonly IDictionary<string, string> nhibernateConfigurationProperties;

        public NHibernateSessionConfiguration(
            string sessionKey, 
            ISessionStorage sessionStorage, 
            IAutoPersistenceModelGenerator autoPersistenceModelGenerator) 
            : this(sessionKey, sessionStorage, autoPersistenceModelGenerator, new Dictionary<string, string>())
        {
        }

        public NHibernateSessionConfiguration(
            string sessionKey, 
            ISessionStorage sessionStorage, 
            IAutoPersistenceModelGenerator autoPersistenceModelGenerator,
            IDictionary<string, string> nhibernateProperties)
        {
            this.SessionKey = sessionKey;
            this.SessionStorage = sessionStorage;
            this.AutoPersistenceModelGenerator = autoPersistenceModelGenerator;
            this.nhibernateConfigurationProperties = this.MergeWithDefaultPropertySet(nhibernateProperties);
        }

        public IAutoPersistenceModelGenerator AutoPersistenceModelGenerator { get; private set; }

        public string SessionKey { get; private set; }

        public ISessionStorage SessionStorage { get; private set; }

        public string[] GetMappingAssemblyPaths()
        {
            return new[] { this.AutoPersistenceModelGenerator.GetType().Assembly.Location.ToLowerInvariant() };
        }

        public IDictionary<string, string> GetPropertyDictionary()
        {
            return this.nhibernateConfigurationProperties;
        }

        private IDictionary<string, string> MergeWithDefaultPropertySet(IDictionary<string, string> nhibernateProperties)
        {
            var defaultDictionary = new Dictionary<string, string>
                {
                    { "connection.connection_string_name", this.SessionKey },
                    { "dialect", "NHibernate.Dialect.MsSql2005Dialect" },
                    { "connection.provider", "NHibernate.Connection.DriverConnectionProvider" },
                    { "connection.driver_class", "NHibernate.Driver.SqlClientDriver" },
                    { "show_sql", "false" },
                    { "connection.release_mode", "auto" },
                    { "adonet.batch_size", "500" },
                    {
                        "proxyfactory.factory_class",
                        "NHibernate.ByteCode.Castle.ProxyFactoryFactory, NHibernate.ByteCode.Castle"
                        },
                };

            foreach (var current in nhibernateProperties)
            {
                defaultDictionary[current.Key] = current.Value;
            }

            return defaultDictionary;
        }
    }
}