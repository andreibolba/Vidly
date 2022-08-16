using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Vidly.Models.EntityFramework
{
    public class Min18YearsIfAMember:ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var costumer = (Costumer)validationContext.ObjectInstance;
            if (costumer.MembershipTypeId == MembershipType.Unkown||costumer.MembershipTypeId==MembershipType.PayAsYouGo)
                return ValidationResult.Success;
            if (costumer.BirthDate == null)
                return new ValidationResult("BirtDate is required");
            var age = DateTime.Now.Year - costumer.BirthDate.Value.Year;
            return (age >= 18) 
                ? ValidationResult.Success 
                : new ValidationResult("You must be at least 18 years old");
        }
    }


}