using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Vidly.Models.IdentityModels;
using Vidly.Models;

namespace Vidly.DataAccess
{
    public class EntityFrameworkMembershipTypeProvider
    {
        private ApplicationDbContext _context;

        public EntityFrameworkMembershipTypeProvider()
        {
            _context = new ApplicationDbContext();
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public void AddMembershipType(Models.MembershipType membershipType)
        {
            _context.MembershipTypes.Add(membershipType);
            _context.SaveChanges();
        }

        public Models.MembershipType GetMembershipType(int id)
        {
            return _context.MembershipTypes.SingleOrDefault(c=>c.Id==id);
        }

        public IEnumerable<Models.MembershipType> GetMembershipTypes()
        {
            return _context.MembershipTypes;
        }

        public void UpdateMembershipType(Models.MembershipType membershipType)
        {
            var membershipTypeInDB = _context.Customers.Single(c => c.Id == membershipType.Id);
            _context.SaveChanges();
        }

        public void DeleteMembershipTypes(Models.MembershipType membershipType)
        {
            _context.MembershipTypes.Remove(membershipType);
            _context.SaveChanges();
        }
    }
}