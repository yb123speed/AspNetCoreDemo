using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ResolveConcurrencyCconflictsWithEFCore.Models
{
    public class MyDbContext:DbContext
    {
        public MyDbContext(DbContextOptions<MyDbContext> Options) : base(Options)
        {
        }
        public DbSet<Post> Posts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //// Fluent API
            //modelBuilder.Entity<Post>()
            //    .Property(p => p.RowVersion).IsConcurrencyToken();

            base.OnModelCreating(modelBuilder);
        }
    }
}
