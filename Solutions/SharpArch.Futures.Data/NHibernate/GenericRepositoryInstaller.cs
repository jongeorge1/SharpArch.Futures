namespace SharpArch.Futures.Data.NHibernate
{
    using Castle.MicroKernel.Registration;
    using Castle.MicroKernel.SubSystems.Configuration;
    using Castle.Windsor;

    using SharpArch.Core.PersistenceSupport;
    using SharpArch.Core.PersistenceSupport.NHibernate;
    using SharpArch.Data.NHibernate;
    using SharpArch.Futures.Core.PersistanceSupport;

    public class GenericRepositoryInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                Component.For<IEntityDuplicateChecker>()
                         .ImplementedBy<EntityDuplicateChecker>()
                         .Named("entityDuplicateChecker"));

            container.Register(
                Component.For(typeof(IRepository<>))
                         .ImplementedBy(typeof(Repository<>))
                         .Named("repositoryType"));

            container.Register(
                Component.For(typeof(INHibernateRepository<>))
                         .ImplementedBy(typeof(NHibernateRepository<>))
                         .Named("nhibernateRepositoryType"));

            container.Register(
                Component.For(typeof(IRepositoryWithTypedId<,>))
                         .ImplementedBy(typeof(RepositoryWithTypedId<,>))
                         .Named("nhibernateRepositoryType"));

            container.Register(
                Component.For(typeof(INHibernateRepositoryWithTypedId<,>))
                         .ImplementedBy(typeof(NHibernateRepositoryWithTypedId<,>))
                         .Named("nhibernateRepositoryType"));

            container.Register(
                Component.For(typeof(ILinqRepository<>))
                         .ImplementedBy(typeof(LinqRepository<>))
                         .Named("repositoryType"));
        }
    }
}