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
    public class ConfigurationsController : ControllerBase
    {
        private readonly LdgsAdminDBContext _context;

        public ConfigurationsController(LdgsAdminDBContext context)
        {
            _context = context;
        }

        // GET: api/{customer}/Configurations
        [HttpGet]
        public async Task<ActionResult<IEnumerable<dbConfiguration>>> GetConfigurations()
        {
            return await _context.Configurations.ToListAsync();
        }

        // GET: api/{customer}/Configurations/5
        [HttpGet("{id}")]
        public async Task<ActionResult<dbConfiguration>> GetConfiguration(int id)
        {
            var configuration = await _context.Configurations.FindAsync(id);

            if (configuration == null)
            {
                return NotFound();
            }

            return configuration;
        }

        // PUT: api/{customer}/Configurations/5
        
        [HttpPut("{id}")]
        public async Task<IActionResult> PutConfiguration(int id, dbConfiguration configuration)
        {
            if (id != configuration.Id)
            {
                return BadRequest();
            }

            _context.Entry(configuration).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ConfigurationExists(id))
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

        // POST: api/{customer}/Configurations
        
        [HttpPost]
        public async Task<ActionResult<dbConfiguration>> PostConfiguration(dbConfiguration configuration)
        {
            _context.Configurations.Add(configuration);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetConfiguration", new { id = configuration.Id }, configuration);
        }

        // DELETE: api/{customer}/Configurations/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteConfiguration(int id)
        {
            var configuration = await _context.Configurations.FindAsync(id);
            if (configuration == null)
            {
                return NotFound();
            }

            _context.Configurations.Remove(configuration);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ConfigurationExists(int id)
        {
            return _context.Configurations.Any(e => e.Id == id);
        }
    }
}
