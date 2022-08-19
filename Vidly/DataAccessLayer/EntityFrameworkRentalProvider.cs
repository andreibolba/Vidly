using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Vidly.Models.IdentityModels;
using Vidly.DataAccessLayer;

namespace Vidly.DataAccess
{
    public class EntityFrameworkRentalProvider
    {
        private ApplicationDbContext _context;

        public EntityFrameworkRentalProvider()
        {
            _context = new ApplicationDbContext();
        }
        public void Dispose()
        {
            _context.Dispose();
        }

        public void AddRental(Models.Rental rental)
        {
            _context.Rentals.Add(rental);
            _context.SaveChanges();
        }

    }
}