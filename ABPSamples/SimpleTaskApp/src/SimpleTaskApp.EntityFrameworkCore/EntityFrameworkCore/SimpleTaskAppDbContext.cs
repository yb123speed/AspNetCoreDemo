using Abp.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace SimpleTaskApp.EntityFrameworkCore
{
    public class SimpleTaskAppDbContext : AbpDbContext
    {
        //Add DbSet properties for your entities...

        public SimpleTaskAppDbContext(DbContextOptions<SimpleTaskAppDbContext> options) 
            : base(options)
        {

        }
    }
}
