using System;
using System.Linq;

namespace Sample.Customers.Database
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var customerDbContext = new CustomerContext())
            {
                customerDbContext.Database.EnsureCreated();
                // Create
                Console.WriteLine("Inserting a new customer");
                customerDbContext.Add(new Customer { FirstName = "John", LastName="Smith", DateOfBirth=DateTime.Parse("14-09-2000") });
                customerDbContext.SaveChanges();

                // Read
                Console.WriteLine("Querying for a customer by Id");
                var customer = customerDbContext.Customers
                    .OrderBy(c => c.Id)
                    .First();
                Console.WriteLine("Found customer with info");
                Console.WriteLine($"Firstname: {customer.FirstName}");

                // Update
                Console.WriteLine("Updating the customer");
                customer.MiddleName = "Henry";
                customerDbContext.SaveChanges();

                // Delete
                Console.WriteLine("Delete the customer");
                customerDbContext.Remove(customer);
                customerDbContext.SaveChanges();
            }
        }
    }
}
