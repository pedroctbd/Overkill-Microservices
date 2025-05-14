using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
namespace UserService.Infra.Data.Factories
{


    public class DbContextFactory : IDesignTimeDbContextFactory<UserDbContext>
    {
        public UserDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<UserDbContext>();

            optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=microservicesOK;Username=postgres;Password=admin");

            return new UserDbContext(optionsBuilder.Options);
        }
    }

    public class EventEnvelopeDbContextFactory : IDesignTimeDbContextFactory<EventEnvelopeDbContext>
    {
        public EventEnvelopeDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<EventEnvelopeDbContext>();

            optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=microservicesOK;Username=postgres;Password=admin");

            return new EventEnvelopeDbContext(optionsBuilder.Options);
        }
    }

}
