using System;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Sample.Customers.Api.Models
{
    public class Customer
    {
        public Guid Id { get; set; }
        public String LastName { get; set; }
        public String MiddleName { get; set; }
        public String FirstName { get; set; }
        public DateTime DateOfBirth { get; set; }
    }

    public class CustomerContext : DbContext
    {
        public CustomerContext(DbContextOptions<CustomerContext> options)
            : base(options)
        {
        }

        public DbSet<Customer> Customers { get; set; }
    }

    public class CustomerValidator : AbstractValidator<Customer>
    {
        public CustomerValidator()
        {
            RuleFor(customer => customer.LastName)
            .NotNull()
            .NotEmpty()
            .MinimumLength(1)
            .MaximumLength(50)
            .WithName("Last Name")
            .WithMessage("{PropertyName} must not be null and must be between 1-50 characters");
            RuleFor(customer => customer.FirstName)
            .NotNull()
            .NotEmpty()
            .MinimumLength(1)
            .MaximumLength(50)
            .WithName("First Name")
            .WithMessage("{PropertyName} must not be null and must be between 1-50 characters");
            RuleFor(customer => customer.DateOfBirth)
            .NotNull()
            .NotEmpty()
            .Must(BeAValidDate)
            .WithName("Date Of Birth")
            .WithMessage("{PropertyName} must not be null or empty and must be before today's date");
        }
        private bool BeAValidDate(DateTime dateOfBirth)
        {
            return (
                dateOfBirth <= DateTime.Today
                );
        }
    }
}