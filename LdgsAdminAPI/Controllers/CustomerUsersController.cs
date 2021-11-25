using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LdgsAdminAPI.DTO.db;
//using LdgsAdminAPI.DbContext;

namespace LdgsAdminAPI.Data
{
    [Route("api/{customer}/[controller]")]
    [ApiController]
    public class CustomerUsersController : ControllerBase
    {
        private readonly LdgsAdminDBContext _context;

        public CustomerUsersController(LdgsAdminDBContext context)
        {
            _context = context;
        }

        // GET: api/{customer}/CustomerUsers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<dbCustomerUser>>> GetCustomerUsers()
        {
            return await _context.CustomerUsers.ToListAsync();
        }

        // GET: api/{customer}/CustomerUsers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<dbCustomerUser>> GetCustomerUser(int id)
        {
            var customerUser = await _context.CustomerUsers.FindAsync(id);

            if (customerUser == null)
            {
                return NotFound();
            }

            return customerUser;
        }

        // PUT: api/{customer}/CustomerUsers/5
        
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCustomerUser(int id, dbCustomerUser customerUser)
        {
            if (id != customerUser.Id)
            {
                return BadRequest();
            }

            _context.Entry(customerUser).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CustomerUserExists(id))
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

        // POST: api/{customer}/CustomerUsers
        
        [HttpPost]
        public async Task<ActionResult<dbCustomerUser>> PostCustomerUser(dbCustomerUser customerUser)
        {
            _context.CustomerUsers.Add(customerUser);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCustomerUser", new { id = customerUser.Id }, customerUser);
        }

        // DELETE: api/{customer}/CustomerUsers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomerUser(int id)
        {
            var customerUser = await _context.CustomerUsers.FindAsync(id);
            if (customerUser == null)
            {
                return NotFound();
            }

            _context.CustomerUsers.Remove(customerUser);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CustomerUserExists(int id)
        {
            return _context.CustomerUsers.Any(e => e.Id == id);
        }
    }
}
