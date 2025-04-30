using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using UserService.Infra.Data.Persistence;
namespace UserService.Infra.Data.Factories
{


    public class UserDbContextFactory : IDesignTimeDbContextFactory<UserDbContext>
    {
        public UserDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<UserDbContext>();

            optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=microservicesOK;Username=postgres;Password=admin");

            return new UserDbContext(optionsBuilder.Options);
        }
    }

}
