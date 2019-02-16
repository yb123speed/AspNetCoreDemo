using Microsoft.EntityFrameworkCore;

namespace SimpleTaskApp.EntityFrameworkCore
{
    public static class DbContextOptionsConfigurer
    {
        public static void Configure(
            DbContextOptionsBuilder<SimpleTaskAppDbContext> dbContextOptions, 
            string connectionString,int type=1
            )
        {
            /* This is the single point to configure DbContextOptions for SimpleTaskAppDbContext */
            switch (type)
            {
                case 0:
                    dbContextOptions.UseSqlServer(connectionString);
                    break;
                case 1:
                    dbContextOptions.UseMySql(connectionString);
                    break;
            }
        }
    }
}
