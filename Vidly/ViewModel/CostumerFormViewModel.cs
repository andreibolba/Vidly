using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Vidly.ViewModel
{
    public class CostumerFormViewModel
    {
        public List<Models.MembershipType> MembershipTypes { get; set; }
        public Models.Costumer Costumer { get; set; }
    }
}