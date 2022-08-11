using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Vidly.Dtos;
using Vidly.Models.IdentityModels;

namespace Vidly.Controllers.Api
{
    public class NewRentalController : ApiController
    {
        public ApplicationDbContext _context;

        public NewRentalController()
        {
            _context = new ApplicationDbContext();
        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
        [HttpPost]
        public IHttpActionResult CreateRental(NewRentalDto newRentalDto)
        {
            Models.Costumer costumer = _context.Customers.Single(c => c.Id == newRentalDto.CostumerId);
            foreach(int id in newRentalDto.MoviesId)
            {
                Models.Movie movie= _context.Movies.ToList().SingleOrDefault(m=>m.Id==id);
                movie.NumberAvailable--;
                Models.Rental rental = new Models.Rental()
                {
                    Costumer = costumer,
                    Movie = movie,
                    DateRented=DateTime.Now
                };
                _context.Rentals.Add(rental);
            }
            _context.SaveChanges();
            return Ok();
        }
    }
}
