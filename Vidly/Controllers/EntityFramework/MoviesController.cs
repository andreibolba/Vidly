using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly.Models;
using Vidly.Models.EntityFramework;
using Vidly.Models.IdentityModels;
using Vidly.ViewModel;

namespace Vidly.Controllers.EntityFramework
{
    public class MoviesController : Controller
    {
        private ApplicationDbContext _context;
        public MoviesController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        public ActionResult AllMovies()
        {
            if (User.IsInRole(RoleName.CanManagerMovies))
                return View("AllMovies");
            return View("ReadOnlyList");
        }
        [Route("Movie/GetMovie/{id}")]
        public ActionResult GetMovie(int id)
        {
            var allMovies = _context.Movies.ToList();
            var movie = allMovies.SingleOrDefault(m => m.Id == id);
            if (User.IsInRole(RoleName.CanManagerMovies))
                return View("GetMovie",movie);
            return View("GetMovieReadOnly", movie);
        }

        // GET: Movie
        public ActionResult Random()
        {
            var movie = new Movie() { Name = "Shrek!" };

            var costumers = new List<Models.EntityFramework.Costumer>
            {
                new Models.EntityFramework.Costumer{Name="Andrei" },
                new Models.EntityFramework.Costumer{Name="Melania"}
            };

            var viewModel = new RandomMovieViewModel
            {
                Movie = movie,
                Costumers = costumers
            };
            return View(viewModel);
        }

        public ActionResult Index(int? pageIndex, string sortBy)
        {
            if (!pageIndex.HasValue)
                pageIndex = 1;
            if (string.IsNullOrWhiteSpace(sortBy))
                sortBy = "Name";

            return Content($"pafeIndex={pageIndex} & sortBy={sortBy}");
        }
        [Route("movies/released/{year:range(1850,2100)}/{month:range(1,12)}")]
        public ActionResult ByReleaseDate(int year, int month)
        {
            return Content($"{year}/{month}");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(Movie movie)
        {
            if (movie.Id == 0)
            {
                movie.Added = DateTime.Now;
                movie.NumberAvailable = movie.Stock;
                _context.Movies.Add(movie);
            }
            else
            {
                var moviesInDb = _context.Movies.Single(m => m.Id == movie.Id);

                //Mapper.Map(costumer,costumerInDb)

                moviesInDb.Name = movie.Name;
                moviesInDb.Released = movie.Released;
                moviesInDb.GenreId = movie.GenreId;
                moviesInDb.Stock = movie.Stock;
                moviesInDb.NumberAvailable = moviesInDb.NumberAvailable;

            }
            _context.SaveChanges();
            return RedirectToAction("AllMovies", "Movies");
        }

        public ActionResult Edit(int id)
        {
            var movie = _context.Movies.SingleOrDefault(c => c.Id == id);
            if (movie == null)
            {
                var viewModel = new MoviesFormViewModel()
                {
                    Genres = _context.Genres.ToList(),
                    Title="New movie"
                };
                if(User.IsInRole(RoleName.CanManagerMovies))
                    return View("MoviesForm", viewModel);
                return Content("not authorized");
            }
            else
            {
                var viewModel = new MoviesFormViewModel(_context.Genres.ToList(),movie)
                {
                    Title = "See movie"
                };
                if (User.IsInRole(RoleName.CanManagerMovies))
                {
                    viewModel.Title = "Edit movie";
                    return View("MoviesForm", viewModel);
                }
                return View("MoviesFormReadOnly", viewModel);
            }
        }
    }
}