using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Vidly.Dtos;
using Vidly.Models;
using System.Data.Entity;
using Vidly.Models.IdentityModels;
using Vidly.Models.EntityFramework;

namespace Vidly.Controllers.Api
{
    public class MoviesController : ApiController
    {
        public ApplicationDbContext _context;

        public MoviesController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        //GET/api/movies
        public IHttpActionResult GetMovies(string query=null)
        {
            var movieQuery = _context.Movies
                .Include(m => m.Genre);
            if (!string.IsNullOrWhiteSpace(query))
                movieQuery = movieQuery.Where(m => m.Name.Contains(query)).Where(m=>m.NumberAvailable>=1);
            var movieDto=movieQuery
                .ToList()
                .Select(Mapper.Map<Movie, MovieDto>);
            return Ok(movieDto);
        }
        //GET/api/movie/1
        public IHttpActionResult GetMovie(int id)
        {
            var movie = _context.Movies.SingleOrDefault(m => m.Id == id);
            if (movie == null)
                return NotFound();
            return Ok(Mapper.Map<Movie, MovieDto>(movie));
        }

        //POST/api/movie
        [HttpPost]
        public IHttpActionResult CreateMovie(MovieDto movieDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            var movie = Mapper.Map<MovieDto, Movie>(movieDto);
            _context.Movies.Add(movie);
            _context.SaveChanges();

            movie.Id = movieDto.Id;

            return Created(new Uri(Request.RequestUri + "/" + movie.Id), movieDto);
        }

        //PUT/api/movie/1
        [HttpPut]
        public void UpdateMovie(int id, MovieDto movieDto)
        {
            if (!ModelState.IsValid)
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            var movie = _context.Movies.SingleOrDefault(m => m.Id == id);
            if (movie == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);
            Mapper.Map<MovieDto, Movie>(movieDto, movie);
            _context.SaveChanges();
        }

        //DELETE/api/movie/1
        [HttpDelete]
        public void DeleteMovie(int id)
        {
            if (User.IsInRole(RoleName.CanManagerMovies))
            {
                var movie = _context.Movies.SingleOrDefault(m => m.Id == id);
                if (movie == null)
                    throw new HttpResponseException(HttpStatusCode.NotFound);
                _context.Movies.Remove(movie);
                _context.SaveChanges();
            }
        }
    }
}
