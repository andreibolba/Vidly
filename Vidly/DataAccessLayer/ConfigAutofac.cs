using Autofac;
using Vidly.DataAccess;

namespace Vidly.DataAccessLayer
{
    public class ConfigAutofac
    {
        public ContainerBuilder builder;

        public ConfigAutofac()
        {
            builder = new ContainerBuilder();
            builder.RegisterType<HibernateProvider>();
            builder.RegisterType<EntityFrameworkCostumerProvider>();
            builder.RegisterType<EntityFrameworkGenreProvider>();
            builder.RegisterType<EntityFrameworkMembershipTypeProvider>();
            builder.RegisterType<EntityFrameworkMoviesProvider>();
            builder.RegisterType<EntityFrameworkRentalProvider>();
        }
    }
}