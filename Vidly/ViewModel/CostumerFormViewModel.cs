using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Vidly.ViewModel
{
    public class CostumerFormViewModel
    {
        public List<Models.EntityFramework.MembershipType> MembershipTypes { get; set; }
        public Models.EntityFramework.Costumer Costumer { get; set; }
    }
}