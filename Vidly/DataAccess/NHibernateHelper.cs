using NHibernate;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate.Tool.hbm2ddl;
using Vidly.Models.NHibernate;
using NHibernate.Context;

namespace Vidly.DataAccess
{
    public class NHibernateHelper
    {
        private static ISessionFactory _sessionFactory;

        private static ISessionFactory SessionFactory
        {
            get
            {
                if (_sessionFactory == null)
                    InitializeSessionFactory(); return _sessionFactory;
            }
        }

        private static void InitializeSessionFactory()
        {
            _sessionFactory=Fluently.Configure().Database(MsSqlConfiguration.MsSql2008
                .ConnectionString("Data Source=andreibo-mbl;Initial Catalog=VidlyStore;Integrated Security=True;MultipleActiveResultSets=True;Application Name=EntityFramework"))
                .Mappings(m => m.FluentMappings
                .AddFromAssemblyOf<CostumerHibernate>())
                .ExposeConfiguration(cfg => new SchemaExport(cfg)
                .Create(true, true))
                .BuildSessionFactory();
        }

        public static ISession OpenSession()
        {
            return SessionFactory.OpenSession();
        }
        public static void CreateSession()
        {
            CurrentSessionContext.Bind(OpenSession());
        }

        public static void CloseSession()
        {
            if (CurrentSessionContext.HasBind(SessionFactory))
            {
                CurrentSessionContext.Unbind(SessionFactory).Dispose();
            }
        }

        public ISession GetCurrentSession()
        {
            if (!CurrentSessionContext.HasBind(SessionFactory))
            {
                CurrentSessionContext.Bind(SessionFactory.OpenSession());
            }
            return SessionFactory.GetCurrentSession();
        }
    }
}