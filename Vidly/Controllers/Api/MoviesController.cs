using AutoMapper;
using Autofac;
using System;
using System.Linq;
using System.Net;
using System.Web.Http;
using Vidly.Dtos;
using Vidly.Models;
using Vidly.DataAccess;
using Vidly.DataAccessLayer;

namespace Vidly.Controllers.Api
{
    public class MoviesController : ApiController
    {
        private ConfigAutofac builder;

        public MoviesController()
        {
            builder = new ConfigAutofac();
        }

        //GET/api/movies
        public IHttpActionResult GetMovies(string query = null)
        {
            using (var c = builder.builder.Build())
            {
                var movieQuery = c.Resolve<EntityFrameworkMoviesProvider>().GetMoviesApi();
                if (!string.IsNullOrWhiteSpace(query))
                    movieQuery = movieQuery.Where(m => m.Name.Contains(query)).Where(m => m.NumberAvailable >= 1);
                var movieDto = movieQuery
                    .ToList()
                    .Select(Mapper.Map<Movie, MovieDto>);
                return Ok(movieDto);
            }
        }
        //GET/api/movie/1
        public IHttpActionResult GetMovie(int id)
        {
            using (var c = builder.builder.Build())
            {
                var movie = c.Resolve<EntityFrameworkMoviesProvider>().GetMovie(id);
                if (movie == null)
                    return NotFound();
                return Ok(Mapper.Map<Movie, MovieDto>(movie));
            }
        }

        //POST/api/movie
        [HttpPost]
        public IHttpActionResult CreateMovie(MovieDto movieDto)
        {
            using (var c = builder.builder.Build())
            {
                if (!ModelState.IsValid)
                    return BadRequest();
                var movie = Mapper.Map<MovieDto, Movie>(movieDto);
                c.Resolve<EntityFrameworkMoviesProvider>().AddMovie(movie);
                movie.Id = movieDto.Id;
                return Created(new Uri(Request.RequestUri + "/" + movie.Id), movieDto);
            }
        }

        //PUT/api/movie/1
        [HttpPut]
        public void UpdateMovie(int id, MovieDto movieDto)
        {
            using (var c = builder.builder.Build())
            {
                if (!ModelState.IsValid)
                    throw new HttpResponseException(HttpStatusCode.BadRequest);
                var movie = c.Resolve<EntityFrameworkMoviesProvider>().GetMovie(id);
                if (movie == null)
                    throw new HttpResponseException(HttpStatusCode.NotFound);
                Mapper.Map<MovieDto, Movie>(movieDto, movie);
            }
        }

        //DELETE/api/movie/1
        [HttpDelete]
        public void DeleteMovie(int id)
        {
            if (User.IsInRole(RoleName.CanManagerMovies))
            {
                using (var c = builder.builder.Build())
                {
                    var movie = c.Resolve<EntityFrameworkMoviesProvider>().GetMovie(id);
                    if (movie == null)
                        throw new HttpResponseException(HttpStatusCode.NotFound);
                    c.Resolve<EntityFrameworkMoviesProvider>().DeleteMovies(id);
                }
            }
        }
    }
}
