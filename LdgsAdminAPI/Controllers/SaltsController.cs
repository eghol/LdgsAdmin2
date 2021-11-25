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
    [Route("api/{customer}/User/Permissions")]
    [ApiController]
    public class SaltsController : ControllerBase
    {
        private readonly LdgsAdminDBContext _context;

        public SaltsController(LdgsAdminDBContext context)
        {
            _context = context;
        }

        // GET: api/{customer}/Salts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<dbSalt>>> GetSalts()
        {
            return await _context.Salts.ToListAsync();
        }

        // GET: api/{customer}/Salts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<dbSalt>> GetSalt(int id)
        {
            var salt = await _context.Salts.FindAsync(id);

            if (salt == null)
            {
                return NotFound();
            }

            return salt;
        }

        // PUT: api/{customer}/Salts/5
        
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSalt(int id, dbSalt salt)
        {
            if (id != salt.Id)
            {
                return BadRequest();
            }

            _context.Entry(salt).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SaltExists(id))
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

        // POST: api/{customer}/Salts
        
        [HttpPost]
        public async Task<ActionResult<dbSalt>> PostSalt(dbSalt salt)
        {
            _context.Salts.Add(salt);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSalt", new { id = salt.Id }, salt);
        }

        // DELETE: api/{customer}/Salts/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSalt(int id)
        {
            var salt = await _context.Salts.FindAsync(id);
            if (salt == null)
            {
                return NotFound();
            }

            _context.Salts.Remove(salt);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SaltExists(int id)
        {
            return _context.Salts.Any(e => e.Id == id);
        }
    }
}
