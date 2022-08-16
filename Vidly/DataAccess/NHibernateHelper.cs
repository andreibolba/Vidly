using NHibernate;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NHibernate.Tool.hbm2ddl;

namespace Vidly.Models.NHibernate
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
    }
}