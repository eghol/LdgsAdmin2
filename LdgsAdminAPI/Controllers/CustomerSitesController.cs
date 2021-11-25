using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LdgsAdminAPI.DTO.db;


namespace LdgsAdminAPI.Data
{
    [Route("api/{customer}/[controller]")]
    [ApiController]
    public class CustomerSitesController : ControllerBase
    {
        private readonly LdgsAdminDBContext _context;

        public CustomerSitesController(LdgsAdminDBContext context)
        {
            _context = context;
        }

        // GET: api/{customer}/CustomerSites
        [HttpGet]
        public async Task<ActionResult<IEnumerable<dbCustomerSite>>> GetCustomerSites()
        {
            return await _context.CustomerSites.ToListAsync();
        }

        // GET: api/{customer}/CustomerSites/5
        [HttpGet("{id}")]
        public async Task<ActionResult<dbCustomerSite>> GetCustomerSite(int id)
        {
            var customerSite = await _context.CustomerSites.FindAsync(id);

            if (customerSite == null)
            {
                return NotFound();
            }

            return customerSite;
        }

        // PUT: api/{customer}/CustomerSites/5
        
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCustomerSite(int id, dbCustomerSite customerSite)
        {
            if (id != customerSite.Id)
            {
                return BadRequest();
            }

            _context.Entry(customerSite).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CustomerSiteExists(id))
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

        // POST: api/{customer}/CustomerSites
        
        [HttpPost]
        public async Task<ActionResult<dbCustomerSite>> PostCustomerSite(dbCustomerSite customerSite)
        {
            _context.CustomerSites.Add(customerSite);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCustomerSite", new { id = customerSite.Id }, customerSite);
        }

        // DELETE: api/{customer}/CustomerSites/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomerSite(int id)
        {
            var customerSite = await _context.CustomerSites.FindAsync(id);
            if (customerSite == null)
            {
                return NotFound();
            }

            _context.CustomerSites.Remove(customerSite);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CustomerSiteExists(int id)
        {
            return _context.CustomerSites.Any(e => e.Id == id);
        }
    }
}
