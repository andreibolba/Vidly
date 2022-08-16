using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Vidly.ViewModelHibernate
{
    public class CostumerHibernateFormViewModel
    {
        public List<NHibernateModels.MembershipTypesHibernate> MembershipTypes { get; set; }
        public int IdM { get; set; }
        public NHibernateModels.CostumersHibernate Costumer { get; set; }
    }
}