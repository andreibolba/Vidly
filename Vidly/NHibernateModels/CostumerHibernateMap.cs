using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Vidly.NHibernateModels
{
    public class CostumerHibernateMap : ClassMap<CostumerHibernate>
    {
        public CostumerHibernateMap()
        {
            Id(x => x.Id);
            Map(x => x.Name);
            Map(x => x.IsSubscribedToNewsletter);
            Map(x => x.MembershipTypeId);
            Map(x => x.BirthDate);
            References(x => x.MembershipType).Cascade.All();
            Table("Costumer");
        }
    }
}