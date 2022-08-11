using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Vidly.Models
{
    public class NewRental
    {
        //public int Id { get; set; }
        public int CostumerId { get; set; }
        public List<int> MoviesId { get; set; }
    }
}