using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using Sample.Customers.Api.Models;

namespace Sample.Customers.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly CustomerContext _context;

        public CustomerController(CustomerContext context)
        {
            _context = context;
        }


        [HttpGet]
        public async Task<List<Customer>> Get()
        {
            return await _context.Customers.ToListAsync();
        }

        [HttpGet("{id}")]
        // Get a customer by Id
        public async Task<Customer> GetById(int? id)
        {
            var customer = await _context.Customers.FindAsync(id);

            return customer;
        }

        // Add a customer
        [HttpPost]
        public async Task<ActionResult<Customer>> AddCustomer(Customer customer)
        {
            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = customer.Id }, customer);
        }


        //Edit a customer
        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateCustomer(int id, Customer customer)
        {
            if (id != customer.Id)
            {
                return BadRequest();
            }

            _context.Entry(customer).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CustomerExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        private Boolean CustomerExists(int id)
        {
            return (GetById(id) == null);
        }

        // //Search for a customer
        [HttpGet("search/{partialName}")]
        public async Task<ActionResult<List<Customer>>> FindCustomer(int id, String partialName)
        {
            if (id == 0)
            {
                var matchingCustomers = await _context.Customers.Where
                    (c =>
                        c.FirstName.Contains(partialName) ||
                        c.LastName.Contains(partialName)).ToListAsync();

                return matchingCustomers;
            }
            else
            {
                var exactMatch = await GetById(id);
                if (exactMatch == null)
                {
                    return NotFound();
                }
                else
                {
                    return new JsonResult(exactMatch);
                }

            }
        }

        //Delete a customer
        [HttpDelete("{id}")]
        public async Task<ActionResult<Customer>> DeleteCustomer(int id)
        {
            var customer = await _context.Customers.FindAsync(id);
            if (customer == null)
            {
                return NotFound();
            }

            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();

            return customer;
        }


    }
}
