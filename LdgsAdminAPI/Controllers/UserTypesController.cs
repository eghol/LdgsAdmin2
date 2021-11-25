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
    public class UserTypesController : ControllerBase
    {
        private readonly LdgsAdminDBContext _context;

        public UserTypesController(LdgsAdminDBContext context)
        {
            _context = context;
        }

        // GET: api/{customer}UserTypes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<dbUserType>>> GetUserTypes()
        {
            return await _context.UserTypes.ToListAsync();
        }
        


        // GET: api/{customer}UserTypes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<dbUserType>> GetUserType(int id)
        {
            var userType = await _context.UserTypes.FindAsync(id);

            if (userType == null)
            {
                return NotFound();
            }

            return userType;
        }

        // PUT: api/{customer}/UserTypes/5
        
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUserType(int id, dbUserType userType)
        {
            if (id != userType.Id)
            {
                return BadRequest();
            }

            _context.Entry(userType).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserTypeExists(id))
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

        // GET: api/{customer}/UserTypes

        [HttpPost]
        public async Task<ActionResult<dbUserType>> PostUserType(dbUserType userType)
        {
            _context.UserTypes.Add(userType);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUserType", new { id = userType.Id }, userType);
        }

        // GET: api/{customer}/UserTypes/5
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteUserType(int id)
        {
            var userType = await _context.UserTypes.FindAsync(id);
            if (userType == null)
            {
                return NotFound();
            }

            _context.UserTypes.Remove(userType);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserTypeExists(int id)
        {
            return _context.UserTypes.Any(e => e.Id == id);
        }


        [NonAction]
        //[HttpGet("{userTypeName:string}")]
        public async Task<int> getUserTypeId(string userTypeName = "")
        {
            try
            {
                dbUserType userType = await _context.UserTypes.Where(e => e.Name.ToLower() == userTypeName.Trim().ToLower()).FirstOrDefaultAsync();
                if (userType == null) return 0;
                return userType.Id;
            }
            catch (Exception e)
            {
                string s = e.Message;
                return 0;
            }
        }
    }
}
