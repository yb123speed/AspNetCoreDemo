using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CQRSSample.Models.SQLite
{
    public class CustomerSQLiteDatabaseContext : DbContext
    {
        public CustomerSQLiteDatabaseContext(DbContextOptions<CustomerSQLiteDatabaseContext> options)
            :base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CustomerRecord>()
                .HasMany(x => x.Phones);
        }

        public DbSet<CustomerRecord> Customers { get; set; }
    }
}
