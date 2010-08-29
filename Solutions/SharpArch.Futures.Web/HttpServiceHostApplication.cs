namespace SharpArch.Futures.Web
{
    using SharpArch.Data.NHibernate;
    using SharpArch.Futures.Core;
    using SharpArch.Wcf.NHibernate;

    public class SharpArchServiceHostApplication : SharpArchApplication
    {
        protected override ISessionStorage GetNewSessionStorage()
        {
            return new WcfSessionStorage();
        }
    }
}