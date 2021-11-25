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
    public class PermissionsController : ControllerBase
    {
        private readonly LdgsAdminDBContext _context;

        public PermissionsController(LdgsAdminDBContext context)
        {
            _context = context;
        }

        // GET: api/{customer}/Permissions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<dbPermission>>> GetPermissions()
        {
            return await _context.Permissions.ToListAsync();
        }

        // GET: api/{customer}/Permissions/5
        [HttpGet("{id}")]
        public async Task<ActionResult<dbPermission>> GetPermission(int id)
        {
            var permission = await _context.Permissions.FindAsync(id);

            if (permission == null)
            {
                return NotFound();
            }

            return permission;
        }

        // PUT: api/{customer}/Permissions/5
        
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPermission(int id, dbPermission permission)
        {
            if (id != permission.Id)
            {
                return BadRequest();
            }

            _context.Entry(permission).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PermissionExists(id))
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

        // POST: api/{customer}/Permissions
        
        [HttpPost]
        public async Task<ActionResult<dbPermission>> PostPermission(dbPermission permission)
        {
            _context.Permissions.Add(permission);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPermission", new { id = permission.Id }, permission);
        }

        // DELETE: api/{customer}/Permissions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePermission(int id)
        {
            var permission = await _context.Permissions.FindAsync(id);
            if (permission == null)
            {
                return NotFound();
            }

            _context.Permissions.Remove(permission);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PermissionExists(int id)
        {
            return _context.Permissions.Any(e => e.Id == id);
        }

        //[NonAction]
        ////[HttpGet("{userTypeName:string}")]
        //public async Task<dbPermissionType> getPermissions(string userId = "")
        //{
        //    try
        //    {
        //        dbPermissionType PermissionType = await _context.PermissionTypes.Where(e => e.Name.ToLower() == Permission.Trim().ToLower()).FirstOrDefaultAsync();
        //        if (PermissionType == null) return null;
        //        return PermissionType;
        //    }
        //    catch (Exception e)
        //    {
        //        return null;
        //    }
        //}
    }
}
