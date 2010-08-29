namespace SharpArch.Futures.Core.Mapping
{
    using Castle.MicroKernel;
    using Castle.Windsor;

    public class MapperInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.AddComponent("oneToOneMapper", typeof(IMapper<,>), typeof(Mapper<,>));

            container.AddComponent("twoToOneMapper", typeof(IMapper<,,>), typeof(Mapper<,,>));
        }
    }
}