﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Vidly.Models;

namespace Vidly.ViewModel
{
    public class RandomMovieViewModel
    {
        public Models.Movie Movie { get; set; }
        public List<Models.Costumer> Costumers { get; set; }
    }
}