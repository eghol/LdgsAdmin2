using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LdgsAdminAPI.Data;
using LdgsAdminAPI.DTO.db;


namespace LdgsAdminAPI.Data
{
    [Route("api/{customer}/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly LdgsAdminDBContext _context;

        public CustomersController(LdgsAdminDBContext context)
        {
            _context = context;
        }

        // GET: api/{customer}/Customers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<dbCustomer>>> GetCustomers()
        {
            return await _context.Customers.ToListAsync();
        }

        // GET: api/{customer}/Customers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<dbCustomer>> GetCustomer(int id)
        {
            var customer = await _context.Customers.FindAsync(id);

            if (customer == null)
            {
                return NotFound();
            }

            return customer;
        }

        // PUT: api/{customer}/Customers/5
        
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCustomer(int id, dbCustomer customer)
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

        // POST: api/{customer}/Customers
        
        [HttpPost]
        public async Task<ActionResult<dbCustomer>> PostCustomer(dbCustomer customer)
        {
            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCustomer", new { id = customer.Id }, customer);
        }

        // DELETE: api/{customer}/Customers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            var customer = await _context.Customers.FindAsync(id);
            if (customer == null)
            {
                return NotFound();
            }

            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CustomerExists(int id)
        {
            return _context.Customers.Any(e => e.Id == id);
        }
    }
}
