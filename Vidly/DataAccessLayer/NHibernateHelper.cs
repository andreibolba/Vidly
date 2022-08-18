using NHibernate;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate.Tool.hbm2ddl;
using NHibernate.Context;
using Vidly.NHibernateModels;
using NHibernate.Cfg;
using NHibernate.Mapping.ByCode;

namespace Vidly.DataAccess
{
    public class NHibernateHelper
    {
        private static ISessionFactory _sessionFactory;

        private static ISessionFactory SessionFactory
        {
            set
            {
                _sessionFactory = value;
            }
            get
            {
                if (_sessionFactory == null)
                    InitializeSessionFactory(); return _sessionFactory;
            }
        }

        private static void InitializeSessionFactory()
        {
            var nhConfig = new Configuration().Configure();
            var mapper = new ConventionModelMapper();


            mapper.BeforeMapManyToOne += (inspector, property, customizer) =>
            {
                customizer.Column($"{property.LocalMember.Name}Id");
                customizer.Lazy(LazyRelation.NoLazy);
                customizer.Insert(false);
                customizer.Update(false);
            };

            mapper.BeforeMapClass += (inspector, type, customizer) =>
            {
                customizer.Id(cMapper => cMapper.Generator(
                                    Generators.HighLow,
                                    gMap => gMap.Params(new
                                    {
                                        table = "NextHighValue",
                                        column = "NextHigh",
                                        max_lo = 99,
                                        where = string.Format("EntityName='{0}'", type.Name)
                                    })
                                    ));

                customizer.Table($"[{type.Name}]");
            };

            var mapping = mapper.CompileMappingFor(new[] { typeof(CostumersHibernate), typeof(MembershipTypesHibernate) });
            nhConfig.AddMapping(mapping);
            nhConfig.AddMapping(mapper.CompileMappingForAllExplicitlyAddedEntities());
            SessionFactory = nhConfig.BuildSessionFactory();
        }

        public static ISession OpenSession()
        {
            return SessionFactory.OpenSession();
        }
        public static void CreateSession()
        {
            CurrentSessionContext.Bind(OpenSession());
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