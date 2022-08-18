using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Vidly.Models.IdentityModels;
using Vidly.Models;
using System.Data.Entity;

namespace Vidly.DataAccess
{
    public class EntityFrameworkMoviesProvider
    {
        private ApplicationDbContext _context;

        public EntityFrameworkMoviesProvider()
        {
            _context = new ApplicationDbContext();
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public void AddMovie(Models.Movie movie)
        {
            _context.Movies.Add(movie);
            _context.SaveChanges();
        }

        public Models.Movie GetMovie(int id)
        {
            return _context.Movies.SingleOrDefault(c=>c.Id==id);
        }

        public IEnumerable<Models.Movie> GetMovies()
        {
            return _context.Movies;
        }
        public IEnumerable<Models.Movie> GetMoviesApi()
        {
            var movieQuery = _context.Movies.AsQueryable();
            return movieQuery.Include(m=>m.Genre);
        }

        public void UpdateMovie(Models.Movie movie)
        {
            var movieInDB = _context.Movies.Single(c => c.Id == movie.Id);
            movieInDB.Name = movie.Name;
            movieInDB.NumberAvailable = movie.NumberAvailable;
            movieInDB.GenreId = movie.GenreId;
            movieInDB.Released = movie.Released;
            movieInDB.Stock = movie.Stock;
            movieInDB.Added = movie.Added;
            _context.SaveChanges();
        }

        public void DeleteMovies(Models.Movie movie)
        {
            _context.Movies.Remove(movie);
            _context.SaveChanges();
        }
    }
}