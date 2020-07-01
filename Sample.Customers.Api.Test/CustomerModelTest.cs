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
        public void LastNameIsValid()
        {
            validator = new CustomerValidator();
            validator.ShouldHaveValidationErrorFor(customer => customer.LastName, null as string);
        }
    }
}
