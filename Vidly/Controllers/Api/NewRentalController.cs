using Autofac;
using System;
using System.Web.Http;
using Vidly.DataAccessLayer;
using Vidly.DataAccess;
using Vidly.Dtos;

namespace Vidly.Controllers.Api
{
    public class NewRentalController : ApiController
    {
        private ConfigAutofac builder;

        public NewRentalController()
        {
            builder = new ConfigAutofac();
        }
        [HttpPost]
        public IHttpActionResult CreateRental(NewRentalDto newRentalDto)
        {
            using (var c = builder.builder.Build())
            {
                Models.Costumer costumer = c.Resolve<EntityFrameworkCostumerProvider>().GetCostumer(newRentalDto.CostumerId);
                foreach (int id in newRentalDto.MoviesId)
                {
                    Models.Movie movie = c.Resolve<EntityFrameworkMoviesProvider>().GetMovie(id);
                    movie.NumberAvailable--;
                    Models.Rental rental = new Models.Rental()
                    {
                        Costumer = costumer,
                        Movie = movie,
                        DateRented = DateTime.Now
                    };
                    c.Resolve<EntityFrameworkRentalProvider>().AddRental(rental);
                }
                return Ok();
            }
        }
    }
}
