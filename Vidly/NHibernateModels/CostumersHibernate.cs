using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Vidly.NHibernateModels
{
    public class CostumersHibernate:HibernateEntity
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
        public virtual bool IsSubscribedToNewsletter { get; set; }
        public virtual MembershipTypesHibernate MembershipTypeHibernate { get; set; }
        public virtual DateTime? BirthDate { get; set; }
        public virtual int MembershipTypeHibernateId { get; set; }
    }
}