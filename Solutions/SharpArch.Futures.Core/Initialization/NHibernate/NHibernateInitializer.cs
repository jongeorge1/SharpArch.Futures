namespace SharpArch.Futures.Core.Initialization.NHibernate
{
    using System;
    using System.Collections.Generic;

    using SharpArch.Data.NHibernate;

    public static class NHibernateInitializer
    {
        public static void Initialize(IEnumerable<NHibernateSessionConfiguration> sessions)
        {
            InitializeAndPersistMappings(sessions, string.Empty);
        }

        public static void InitializeAndPersistMappings(
            IEnumerable<NHibernateSessionConfiguration> sessions, string mappingsTargetFolder)
        {
            Data.NHibernate.NHibernateInitializer.Instance().InitializeNHibernateOnce(
                () => InitialiseNHibernateSessions(sessions, mappingsTargetFolder));
        }

        private static void InitialiseDefaultNHibernateSession(
            NHibernateSessionConfiguration session, string mappingsTargetFolder)
        {
            var autoPersistenceModel = session.AutoPersistenceModelGenerator.Generate();

            if (!string.IsNullOrEmpty(mappingsTargetFolder))
            {
                autoPersistenceModel.WriteMappingsTo(mappingsTargetFolder);
            }

            NHibernateSession.Init(
                session.SessionStorage, 
                session.GetMappingAssemblyPaths(), 
                autoPersistenceModel, 
                string.Empty, 
                session.GetPropertyDictionary(), 
                string.Empty);
        }

        private static void InitialiseNHibernateSessions(
            IEnumerable<NHibernateSessionConfiguration> sessions, string mappingsTargetFolder)
        {
            var isFirst = true;

            foreach (var session in sessions)
            {
                if (isFirst)
                {
                    // Add single session with default key.
                    InitialiseDefaultNHibernateSession(session, mappingsTargetFolder);
                    isFirst = false;
                }
                else
                {
                    throw new InvalidOperationException(
                        "Support for multiple databases has not yet been implemented in Zopa.Infrastructure.Initialization.NHibernateInitializer");
                }
            }
        }
    }
}