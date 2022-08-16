using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Vidly.Models.EntityFramework
{
    public class Costumer
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="Please enter a name")]
        [StringLength(255)]
        public string Name { get; set; }
        public bool IsSubscribedToNewsletter { get; set; }
        [Display(Name = "Membership Type")]
        public MembershipType MembershipType { get; set; }
        public byte MembershipTypeId { get; set; }
        [Display(Name = "Birth date")]
        [Min18YearsIfAMember]
        public DateTime? BirthDate { get; set; }

    }
}