using System;
using FluentValidation;
namespace Sample.Customers.Database.Models
{
    public class Customer
    {
        public Guid Id { get; set; }
        public String FirstName { get; set; }
        public String MiddleName { get; set; }
        public String LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
    }

    public class CustomerValidator : AbstractValidator<Customer>
    {
        public CustomerValidator()
        {
            RuleFor(customer => customer.LastName).NotNull();
            RuleFor(customer => customer.FirstName).NotNull();
            RuleFor(customer => customer.DateOfBirth).NotNull();

        }
    }
}