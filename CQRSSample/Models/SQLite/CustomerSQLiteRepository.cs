using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CQRSSample.Models.SQLite
{
    public class CustomerSQLiteRepository
    {
        private readonly CustomerSQLiteDatabaseContext _context;
        public CustomerSQLiteRepository(CustomerSQLiteDatabaseContext context)
        {
            _context = context;
        }
        public CustomerRecord Create(CustomerRecord customer)
        {
            Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry<CustomerRecord> entry = _context.Customers.Add(customer);
            _context.SaveChanges();
            return entry.Entity;
        }
        public void Update(CustomerRecord customer)
        {
            _context.SaveChanges();
        }
        public void Remove(long id)
        {
            _context.Customers.Remove(GetById(id));
            _context.SaveChanges();
        }
        public IQueryable<CustomerRecord> GetAll()
        {
            return _context.Customers;
        }
        public CustomerRecord GetById(long id)
        {
            return _context.Customers.Find(id);
        }
    }
}
