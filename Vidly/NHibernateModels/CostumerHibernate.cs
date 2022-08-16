using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Vidly.NHibernateModels
{
    public class CostumerHibernate
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
        public virtual bool IsSubscribedToNewsletter { get; set; }
        public virtual MembershipType MembershipType { get; set; }
        public virtual byte MembershipTypeId { get; set; }
        public virtual DateTime? BirthDate { get; set; }
    }
}