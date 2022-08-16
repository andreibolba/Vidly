using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Vidly.NHibernateModels
{
    public class MembershipTypesHibernateMap : ClassMap<MembershipTypesHibernate>
    {
        public MembershipTypesHibernateMap()
        {
            Id(x => x.Id);
            Map(x => x.Name);
            Map(x => x.DurationInMonth);
            Map(x => x.SignUpFee);
            Map(x => x.Discount);
            Table("MembershipTypesHibernate");
        }
    }
}