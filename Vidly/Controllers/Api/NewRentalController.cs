using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Vidly.DataAccess;
using Vidly.Dtos;
using Vidly.Models.IdentityModels;

namespace Vidly.Controllers.Api
{
    public class NewRentalController : ApiController
    {
        private EntityFrameworkCostumerProvider providerCostumers;
        private EntityFrameworkMembershipTypeProvider providerMembership;
        private EntityFrameworkMoviesProvider providerMovies;
        private EntityFrameworkGenreProvider providerGenre;
        private EntityFrameworkRentalProvider providerRental;

        public NewRentalController()
        {
            providerCostumers = new EntityFrameworkCostumerProvider();
            providerMembership = new EntityFrameworkMembershipTypeProvider();
            providerMovies = new EntityFrameworkMoviesProvider();
            providerGenre = new EntityFrameworkGenreProvider();
            providerRental = new EntityFrameworkRentalProvider();
        }

        protected override void Dispose(bool disposing)
        {
            providerCostumers.Dispose();
            providerMembership.Dispose();
            providerMovies.Dispose();
            providerGenre.Dispose();
            providerRental.Dispose();
        }
        [HttpPost]
        public IHttpActionResult CreateRental(NewRentalDto newRentalDto)
        {
            Models.Costumer costumer = providerCostumers.GetCostumer(newRentalDto.CostumerId);
            foreach(int id in newRentalDto.MoviesId)
            {
                Models.Movie movie = providerMovies.GetMovie(id);
                movie.NumberAvailable--;
                Models.Rental rental = new Models.Rental()
                {
                    Costumer = costumer,
                    Movie = movie,
                    DateRented=DateTime.Now
                };
                providerRental.AddRental(rental);
            }
            return Ok();
        }
    }
}
