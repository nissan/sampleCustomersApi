using System;
using System.Linq;
using Sample.Customers.Database.Models;
using FluentValidation;

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
                Customer customer = new Customer
                {
                    FirstName = "John",
                    LastName = "Smith",
                    DateOfBirth = DateTime.Parse("14-09-2000")
                };
                CustomerValidator customerValidator = new CustomerValidator();
                var results = customerValidator.Validate(customer);
                if (!results.IsValid)
                {
                    foreach (var failure in results.Errors)
                    {
                        Console.WriteLine("Property " + failure.PropertyName + " failed validation. Error was: " + failure.ErrorMessage);
                    }
                }
                else
                {
                    customerDbContext.Add(customer);
                    customerDbContext.SaveChanges();
                }

                // Read
                Console.WriteLine("Querying for a customer by Id");
                var customerLookup = customerDbContext.Customers
                    .OrderBy(c => c.Id)
                    .First();
                Console.WriteLine("Found customer with info");
                Console.WriteLine($"Firstname: {customerLookup.FirstName}");

                // Update
                Console.WriteLine("Updating the customer");
                customerLookup.MiddleName = "Henry";
                customerDbContext.SaveChanges();

                // Delete
                Console.WriteLine("Delete the customer");
                customerDbContext.Remove(customerLookup);
                customerDbContext.SaveChanges();
            }
        }
    }
}
