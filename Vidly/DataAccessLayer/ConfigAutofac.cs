using Autofac;
using Vidly.DataAccess;

namespace Vidly.DataAccessLayer
{
    public class ConfigAutofac
    {
        public ContainerBuilder Builder;

        public ConfigAutofac()
        {
            Builder = new ContainerBuilder();
            Builder.RegisterType<HibernateProvider>();
            Builder.RegisterType<EntityFrameworkCostumerProvider>();
            Builder.RegisterType<EntityFrameworkGenreProvider>();
            Builder.RegisterType<EntityFrameworkMembershipTypeProvider>();
            Builder.RegisterType<EntityFrameworkMoviesProvider>();
            Builder.RegisterType<EntityFrameworkRentalProvider>();
        }
    }
}