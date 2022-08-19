using Autofac;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly.DataAccess;
using Vidly.DataAccessLayer;
using Vidly.Models;
using Vidly.Models.IdentityModels;
using Vidly.ViewModel;

namespace Vidly.Controllers
{
    public class MoviesController : Controller
    {
        private ConfigAutofac builder;

        public MoviesController()
        {
            builder = new ConfigAutofac();
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
            using (var c = builder.builder.Build())
            {
                var movie = c.Resolve<EntityFrameworkMoviesProvider>().GetMovie(id);
                if (User.IsInRole(RoleName.CanManagerMovies))
                    return View("GetMovie", movie);
                return View("GetMovieReadOnly", movie);
            }
        }

        // GET: Movie
        public ActionResult Random()
        {
            var movie = new Movie() { Name = "Shrek!" };

            var costumers = new List<Models.Costumer>
            {
                new Models.Costumer{Name="Andrei" },
                new Models.Costumer{Name="Melania"}
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
            using (var c = builder.builder.Build())
            {
                if (movie.Id == 0)
                {
                    movie.Added = DateTime.Now;
                    movie.NumberAvailable = movie.Stock;
                    c.Resolve<EntityFrameworkMoviesProvider>().AddMovie(movie);
                }
                else
                {
                    var movieFromDb = c.Resolve<EntityFrameworkMoviesProvider>().GetMovie(movie.Id);
                    movie.Added = movieFromDb.Added;
                    c.Resolve<EntityFrameworkMoviesProvider>().UpdateMovie(movie);
                }
                return RedirectToAction("AllMovies", "Movies");
            }
        }

        public ActionResult Edit(int id)
        {
            using (var c = builder.builder.Build())
            {
                var movie = c.Resolve<EntityFrameworkMoviesProvider>().GetMovie(id);
                if (movie == null)
                {
                    var viewModel = new MoviesFormViewModel()
                    {
                        Genres = c.Resolve<EntityFrameworkGenreProvider>().GetGenres().ToList(),
                        Title = "New movie"
                    };
                    if (User.IsInRole(RoleName.CanManagerMovies))
                        return View("MoviesForm", viewModel);
                    return Content("not authorized");
                }
                else
                {
                    var viewModel = new MoviesFormViewModel(c.Resolve<EntityFrameworkGenreProvider>().GetGenres().ToList(), movie)
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
}