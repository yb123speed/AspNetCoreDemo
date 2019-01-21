using SimpleTaskApp.Configuration;
using SimpleTaskApp.Web;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace SimpleTaskApp.EntityFrameworkCore
{
    /* This class is needed to run EF Core PMC commands. Not used anywhere else */
    public class SimpleTaskAppDbContextFactory : IDesignTimeDbContextFactory<SimpleTaskAppDbContext>
    {
        public SimpleTaskAppDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<SimpleTaskAppDbContext>();
            var configuration = AppConfigurations.Get(WebContentDirectoryFinder.CalculateContentRootFolder());

            DbContextOptionsConfigurer.Configure(
                builder,
                configuration.GetConnectionString(SimpleTaskAppConsts.ConnectionStringName)
            );

            return new SimpleTaskAppDbContext(builder.Options);
        }
    }
}