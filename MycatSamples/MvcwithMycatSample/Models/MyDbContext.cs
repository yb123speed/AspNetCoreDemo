using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;

namespace MvcwithEFMycatSample.Models
{
    public class MyDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            //optionsBuilder
            //    .UseMySql("server=192.168.1.180;port=8066;uid=root;pwd=123456;database=testdb");
                //.UseDataNode("192.168.0.102", "dn", "root", "19931101")
                //.UseDataNode("192.168.0.102", "mycatblog2", "root", "19931101");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().ToTable("dt_users");

        }
    }
}