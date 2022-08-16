using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Vidly.Models.NHibernate
{
    public class MembershipTypeHibernate
    {
        public virtual byte Id { get; set; }
        public virtual string Name { get; set; }
        public virtual short SignUpFee { get; set; }
        public virtual byte DurationInMonth { get; set; }
        public virtual byte Discount { get; set; }

    }
}