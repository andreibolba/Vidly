using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Vidly.Dtos
{
    public class NewRentalDto
    {
        //public int Id { get; set; }
        public int CostumerId { get; set; }
        public List<int> MoviesId { get; set; }
    }
}