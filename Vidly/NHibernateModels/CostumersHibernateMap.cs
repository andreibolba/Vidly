using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Vidly.NHibernateModels
{
    public class CostumersHibernateMap : ClassMap<CostumersHibernate>
    {
        public CostumersHibernateMap()
        {
            Id(x => x.Id);
            Map(x => x.Name);
            Map(x => x.IsSubscribedToNewsletter);
            Map(x => x.BirthDate);
            References(x => x.MembershipTypeHibernate).Cascade.All();
            Table("CostumersHibernate");
        }
    }
}