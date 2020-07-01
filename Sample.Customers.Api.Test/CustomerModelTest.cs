using System;
using Xunit;
using FluentValidation;
using FluentValidation.TestHelper;
using Sample.Customers.Api.Models;

namespace Sample.Customers.Api.Test
{
    public class CustomerModelTest
    {
        private CustomerValidator validator;
        [Fact]
        public void Should_have_error_when_last_name_is_null_or_empty()
        {
            validator = new CustomerValidator();
            validator.ShouldHaveValidationErrorFor(customer => customer.LastName, null as string);
            validator.ShouldHaveValidationErrorFor(customer => customer.LastName, String.Empty);

        }

        [Fact]
        public void Should_not_have_error_when_last_name_is_specified()
        {
            validator = new CustomerValidator();
            validator.ShouldNotHaveValidationErrorFor(customer => customer.LastName, "Howard");

        }

        [Fact]
        public void Should_have_error_when_first_name_is_null()
        {
            validator = new CustomerValidator();
            validator.ShouldHaveValidationErrorFor(customer => customer.LastName, null as string);
            validator.ShouldHaveValidationErrorFor(customer => customer.FirstName, String.Empty);

        }

        [Fact]
        public void Should_not_have_error_when_first_name_is_specified()
        {
            validator = new CustomerValidator();
            validator.ShouldNotHaveValidationErrorFor(customer => customer.LastName, "Jeremy");
        }

        [Fact]
        public void Should_not_have_error_when_middle_name_is_null_or_empty()
        {
            validator = new CustomerValidator();
            validator.ShouldNotHaveValidationErrorFor(customer => customer.MiddleName, null as string);
            validator.ShouldHaveValidationErrorFor(customer => customer.FirstName, String.Empty);

        }

        [Fact]
        public void Should_not_have_error_when_middle_name_is_specified()
        {
            validator = new CustomerValidator();
            validator.ShouldNotHaveValidationErrorFor(customer => customer.MiddleName, "Matt");
        }

        [Fact]
        public void Should_have_error_when_date_of_birth_is_null_or_empty()
        {
            validator = new CustomerValidator();
            if (DateTime.TryParse(null, out var dateOfBirth))
                validator.ShouldHaveValidationErrorFor(customer => customer.DateOfBirth, dateOfBirth);
            if (DateTime.TryParse(String.Empty, out var dateOfBirth2))
                validator.ShouldHaveValidationErrorFor(customer => customer.DateOfBirth, dateOfBirth2);
        }

        [Fact]
        public void Should_have_error_when_date_of_birth_is_in_the_future()
        {
            validator = new CustomerValidator();
            validator.ShouldHaveValidationErrorFor(customer => customer.DateOfBirth, DateTime.Today.AddDays(1));
        }

        [Fact]
        public void Should_not_have_error_when_date_of_birth_is_specified()
        {
            validator = new CustomerValidator();
            validator.ShouldNotHaveValidationErrorFor(customer => customer.DateOfBirth, DateTime.Parse("20-12-2000"));
        }
    }
}
