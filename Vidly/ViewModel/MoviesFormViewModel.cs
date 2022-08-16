using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Vidly.ViewModel
{
    public class MoviesFormViewModel
    {
        public List<Models.EntityFramework.Genre> Genres { get; set; }
        public int? Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Display(Name = "Release date")]
        [Required]
        public DateTime? Released { get; set; }
        [Range(1, 20)]
        public int? Stock { get; set; }
        [Required]
        public byte? GenreId { get; set; }

        public string Title { get; set; }
        public string GenreName { get; set; }

        public MoviesFormViewModel()
        {
            Id = 0;
        }

        public MoviesFormViewModel(List<Models.EntityFramework.Genre> Genres,Models.EntityFramework.Movie movie)
        {
            this.Genres = Genres;
            Id = movie.Id;
            Name = movie.Name;
            Released = movie.Released;
            Stock = movie.Stock;
            GenreId = movie.GenreId;
            GenreName = Genres.SingleOrDefault(g => g.Id == GenreId).Name;
        }
    }
}