using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LdgsAdminAPI.DTO.db;
using Microsoft.Extensions.Configuration;


namespace LdgsAdminAPI.Data
{
    [Route("api/{customer}/[controller]")]
    [ApiController]
    public class SitesController : ControllerBase
    {
        private readonly LdgsAdminDBContext _context;
        private readonly IConfiguration _config;
        public SitesController(LdgsAdminDBContext context,IConfiguration config)
        {
            _context = context;
            _config = config;
        }

        // ex på funktionell API från kurs -> https://app.pluralsight.com/course-player?clipId=86cc7e83-3577-4581-bb0a-117147fa4011
        [HttpOptions("reloadconfig")] // 
        public ActionResult ReloadConfig()
        {
            try
            {
                var root = (IConfigurationRoot)_config;
                root.Reload();
                return Ok();
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        // GET: api/{customer}/Sites
        [HttpGet]
        public async Task<ActionResult<IEnumerable<dbSite>>> GetSites()
        {
            return await _context.Sites.ToListAsync();
        }

        // GET: api/{customer}/Sites/5
        [HttpGet("{id}")]
        public async Task<ActionResult<dbSite>> GetSite(int id)
        {
            var site = await _context.Sites.FindAsync(id);

            if (site == null)
            {
                return NotFound();
            }

            return site;
        }

        // PUT: api/{customer}/Sites/5
        
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSite(int id, dbSite site)
        {
            if (id != site.Id)
            {
                return BadRequest();
            }

            _context.Entry(site).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SiteExists(id))
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

        // POST: api/{customer}/Sites
        
        [HttpPost]
        public async Task<ActionResult<dbSite>> PostSite(dbSite site)
        {
            _context.Sites.Add(site);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSite", new { id = site.Id }, site);
        }

        // DELETE: api/{customer}/Sites/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSite(int id)
        {
            var site = await _context.Sites.FindAsync(id);
            if (site == null)
            {
                return NotFound();
            }

            _context.Sites.Remove(site);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SiteExists(int id)
        {
            return _context.Sites.Any(e => e.Id == id);
        }
    }
}
