using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Vidly.ViewModelHibernate
{
    public class CostumerHibernateFormViewModel
    {
        public List<NHibernateModels.MembershipTypesHibernate> MembershipTypes { get; set; }
        public List<NHibernateModels.CostumersHibernate> Costumers { get; set; }
        public bool CanManageCostumers { get; set; }
        public NHibernateModels.CostumersHibernate Costumer { get; set; }
    }
}