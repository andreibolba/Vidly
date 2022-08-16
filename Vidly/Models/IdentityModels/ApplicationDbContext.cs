using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Vidly.Models.IdentityModels
{
    public class ApplicationDbContext : IdentityDbContext<Models.IdentityModels.ApplicationUser>
    {
        public DbSet<Models.Costumer> Customers { get; set; }
        public DbSet<Models.Movie> Movies { get; set; }
        public DbSet<Models.MembershipType> MembershipTypes { get; set; }
        public DbSet<Models.Genre> Genres { get; set; }
        public DbSet<Models.Rental> Rentals { get; set; }

        public ApplicationDbContext()
            : base("Data Source=ANDREIBO-MBL;Initial Catalog=VidlyStore;Integrated Security=SSPI;", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}