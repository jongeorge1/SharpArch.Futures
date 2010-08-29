namespace SharpArch.Futures.Web
{
    using System;

    using SharpArch.Data.NHibernate;
    using SharpArch.Futures.Core;
    using SharpArch.Web.NHibernate;

    public class SharpArchWebApplication : SharpArchApplication
    {
        protected override ISessionStorage GetNewSessionStorage()
        {
            return new WebSessionStorage(this);
        }
    }
}