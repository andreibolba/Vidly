using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Vidly.NHibernateModels
{
    public class MembershipTypeHibernateMap : ClassMap<MembershipTypeHibernate>
    {
        public MembershipTypeHibernateMap()
        {
            Id(x => x.Id);
            Map(x => x.Name);
            Map(x => x.DurationInMonth);
            Map(x => x.SignUpFee);
            Map(x => x.Discount);
            Table("MembershipType");
        }
    }
}