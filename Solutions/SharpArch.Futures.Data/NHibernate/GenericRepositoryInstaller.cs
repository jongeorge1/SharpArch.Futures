namespace SharpArch.Futures.Data.NHibernate
{
    using Castle.MicroKernel;
    using Castle.Windsor;

    using SharpArch.Core.PersistenceSupport;
    using SharpArch.Core.PersistenceSupport.NHibernate;
    using SharpArch.Data.NHibernate;
    using SharpArch.Futures.Core.PersistanceSupport;

    public class GenericRepositoryInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.AddComponent(
                    "entityDuplicateChecker",
                    typeof(IEntityDuplicateChecker),
                    typeof(EntityDuplicateChecker));

            container.AddComponent(
                    "repositoryType",
                    typeof(IRepository<>),
                    typeof(Repository<>));

            container.AddComponent(
                    "nhibernateRepositoryType",
                    typeof(INHibernateRepository<>),
                    typeof(NHibernateRepository<>));

            container.AddComponent(
                    "repositoryWithTypedId",
                    typeof(IRepositoryWithTypedId<,>),
                    typeof(RepositoryWithTypedId<,>));

            container.AddComponent(
                    "nhibernateRepositoryWithTypedId",
                    typeof(INHibernateRepositoryWithTypedId<,>),
                    typeof(NHibernateRepositoryWithTypedId<,>));

            container.AddComponent(
                "linqRepository",
                typeof(ILinqRepository<>),
                typeof(LinqRepository<>));
        }
    }
}