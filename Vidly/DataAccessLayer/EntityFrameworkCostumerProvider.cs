using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Vidly.Models.IdentityModels;
using Vidly.Models;
using System.Data.Entity;

namespace Vidly.DataAccess
{
    public class EntityFrameworkCostumerProvider
    {
        private ApplicationDbContext _context;

        public EntityFrameworkCostumerProvider()
        {
            _context = new ApplicationDbContext();
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public void AddCostumer(Models.Costumer costumer)
        {
            _context.Customers.Add(costumer);
            _context.SaveChanges();
        }

        public Models.Costumer GetCostumer(int id)
        {
            return _context.Customers.SingleOrDefault(c=>c.Id==id);
        }

        public IEnumerable<Models.Costumer> GetCostumers()
        {
            return _context.Customers;
        }

        public IEnumerable<Models.Costumer> GetCostumersApi()
        {
            var costumerQuery = _context.Customers.AsQueryable();
            return costumerQuery.Include(c => c.MembershipType);
        }

        public void UpdateCostumer(Models.Costumer costumer)
        {
            var costumerInDB = _context.Customers.Single(c => c.Id == costumer.Id);
            costumerInDB.Name = costumer.Name;
            costumerInDB.BirthDate = costumer.BirthDate;
            costumerInDB.MembershipTypeId = costumer.MembershipTypeId;
            costumerInDB.IsSubscribedToNewsletter = costumer.IsSubscribedToNewsletter;
            _context.SaveChanges();
        }

        public void DeleteCostumers(int id)
        {
            var c = GetCostumer(id);
            _context.Customers.Remove(c);
            _context.SaveChanges();
        }
    }
}