namespace SharpArch.Futures.Core.Mapping
{
    using Castle.MicroKernel.Registration;
    using Castle.MicroKernel.SubSystems.Configuration;
    using Castle.Windsor;

    public class MapperInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Component.For(typeof(IMapper<,>)).ImplementedBy(typeof(Mapper<,>)).Named("oneToOneMapper"));
            container.Register(Component.For(typeof(IMapper<,,>)).ImplementedBy(typeof(Mapper<,,>)).Named("oneToTwoMapper"));
        }
    }
}