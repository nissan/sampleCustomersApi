using Microsoft.EntityFrameworkCore;
using Sample.Customers.Database.Models;
namespace Sample.Customers.Database
{
    public class CustomerContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite("Data Source=customers.db");
    }
}