using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Vidly.Models.EntityFramework
{
    public class Rental
    {
        public int Id { get; set; }
        [Required]
        public Models.EntityFramework.Costumer Costumer { get; set; }
        [Required]
        public Models.EntityFramework.Movie Movie { get; set; }
        public DateTime DateRented { get; set; }
        public DateTime? DateReturned { get; set; }
    }
}