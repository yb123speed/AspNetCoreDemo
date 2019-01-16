using Abp.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SimpleTaskApp.Tasks;

namespace SimpleTaskApp.EntityFrameworkCore
{
    public class SimpleTaskAppDbContext : AbpDbContext
    {
        //Add DbSet properties for your entities...
        public DbSet<Task> Tasks { get; set; }

        public SimpleTaskAppDbContext(DbContextOptions<SimpleTaskAppDbContext> options) 
            : base(options)
        {

        }
    }
}
