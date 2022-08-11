using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Vidly.Models;

namespace Vidly.Dtos
{
    public class MovieDto
    {
        public int Id { get; set; }
        [StringLength(255)]
        public string Name { get; set; }
        public DateTime Added { get; set; }
        public DateTime Released { get; set; }
        public int Stock { get; set; }
        public GenreDto Genre { get; set; }
        public byte GenreId { get; set; }
    }
}