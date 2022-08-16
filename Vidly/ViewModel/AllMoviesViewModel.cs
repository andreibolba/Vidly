using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Vidly.ViewModel
{
    public class AllMoviesViewModel
    {
        public List<Models.EntityFramework.Movie> Movies { get; set; }
    }
}