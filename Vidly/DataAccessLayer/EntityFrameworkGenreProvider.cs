using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Vidly.Models.IdentityModels;
using Vidly.Models;

namespace Vidly.DataAccess
{
    public class EntityFrameworkGenreProvider
    {
        private ApplicationDbContext _context;

        public EntityFrameworkGenreProvider()
        {
            _context = new ApplicationDbContext();
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public void AddGenre(Models.Genre genre)
        {
            _context.Genres.Add(genre);
            _context.SaveChanges();
        }

        public Models.Genre GetGenre(int id)
        {
            return _context.Genres.SingleOrDefault(c=>c.Id==id);
        }

        public IEnumerable<Models.Genre> GetGenres()
        {
            return _context.Genres;
        }

        public void UpdateGenre(Models.Genre genre)
        {
            var genresInDB = _context.Genres.Single(c => c.Id == genre.Id);
            genresInDB.Name = genre.Name;
            _context.SaveChanges();
        }

        public void DeleteGenres(Models.Genre genre)
        {
            _context.Genres.Remove(genre);
            _context.SaveChanges();
        }
    }
}