using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LdgsAdminAPI.DTO.db;
using LdgsAdminAPI.DTO.request;
using LdgsAdminAPI.DTO.response;
using LdgsAdminAPI.ObjectMapper;
using AutoMapper;
using Microsoft.AspNetCore.Routing;

namespace LdgsAdminAPI.Data
{
    [Route("api/{customer}/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly LdgsAdminDBContext _context;
        private readonly IMapper _Mapper;
        private readonly LinkGenerator _linkGenerator;
        public UsersController(LdgsAdminDBContext context, IMapper mapper,LinkGenerator linkGenerator)
        {
            _context = context;
           _Mapper = mapper;
            _linkGenerator = linkGenerator;
        }

        // GET: api/{customer}/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<dbUser>>> GetUsers()
        {
            return await _context.Users.ToListAsync();
        }





        // GET: api/{customer}/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<dbUser>> GetUser(int id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        // PUT: api/{customer}/Users/5
        
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(int id, dbUser user)
        {
            if (id != user.Id)
            {
                return BadRequest();
            }

            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
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

       

        [HttpPost]
        public async Task<ActionResult<resUser>> AddUser(reqUser user)
        {
            string chkValidMsg = await isValid(user);
            if (chkValidMsg != "") return BadRequest("AddUser failed on method isValid() -> " + chkValidMsg);            

            var ut = new UserTypesController(_context);
            // Try to find Id for given UserType
            user.UserTypeId = await ut.getUserTypeId(user.UserType);
            // did we found it?
            if (user.UserTypeId == 0) return BadRequest($"AddUser failed, UserType is not valid : {user.UserType}");

            // Yes ! populate db-item from given object
            dbUser dbItem = objMapper.Mapper.Map<dbUser>(user);


            _context.Users.Add(dbItem);

            if (await _context.SaveChangesAsync() > 0)
            {
                if (user.Permissions != null)
                {
                    var pts = new PermissionTypesController(_context);
                    dbPermission lstPermissions = new dbPermission();
                    bool savePermissions = false;
                    foreach (string pt in user.Permissions)
                    {
                        dbPermissionType p = await pts.getPermissionByName(pt);
                        if (p != null) {                             
                            dbItem.Permissions.Add(new dbPermission { UserId = dbItem.Id, PermissionTypeId = p.Id }); 
                            savePermissions = true;
                        }
                    }
                    if (savePermissions) await _context.SaveChangesAsync();
                }

                resUser newUser = new resUser();
                newUser.Permissions = dbItem.Permissions.ToList();
               // _context.Entry(user).State = EntityState.Modified;
                //await _context.SaveChangesAsync();
                //newUser.UserType = user.UserType;
                return newUser;
            } else
            {
                return BadRequest("Save new data failed");
            }

            

            //var location = _linkGenerator.GetPathByAction("Get", "userType", new { userTypeName = user.UserType });
            //if (string.IsNullOrEmpty(location))
            //{
            //    return BadRequest($"Could not use current UserTypID : {user.UserTypeId}");
            //}
            // save new user to DB 

        }

        //// POST: api/{customer}/Users        
        //[HttpPost]
        //public async Task<ActionResult<dbUser>> PostUser(dbUser user)
        //{
        //    _context.Users.Add(user);
        //    await _context.SaveChangesAsync();

        //    return CreatedAtAction("GetUser", new { id = user.Id }, user);
        //}

        // DELETE: api/{customer}/Users/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }


        // ---------------------------------------------------------------              Extra Tool to handel User


        // Test if User exist in DB
        private bool UserExists(int id)
        {
            return _context.Users.Any(e => e.Id == id);
        }


        [NonAction]
        /*  Test if given userName exist in DB.
            Return : int greater than zero if found (userId )
                     else returns zero = user does not exist
                     
            */
        public async Task<int> UserNameExists(string userName = "")
        {
            try
            {
                dbUser userType = await _context.Users.Where(e => e.Username.ToLower() == userName.Trim().ToLower()).FirstOrDefaultAsync();
                if (userType == null) return 0;
                return userType.Id;
            }
            catch (Exception e)
            {
                string s = e.Message;
                return 0;
            }
        }


        
        [NonAction]
        /*
         * Function: validat that necessary data are complete/not missing
         *           Ment to be used before a POST of a new User(basic) to DB
         * 
         * Return : string ="" -> All data is Ok
         *          string !="" -> Missing some or alla necessary data. The
         *          value that's return is qe to the param:s that are missing
        */
        public async Task<string> isValid(reqUser user)
        {
            string errMsg = "";

            // Username set?
            if (user.Username.Trim() == "") errMsg = errMsg + "Missing Username,";

            // User already exist ?
            if (await UserNameExists(user.Username) > 0) errMsg = errMsg + "User already exist ,";

            // UserType set ?
            if (user.UserType.Trim() == "") errMsg = errMsg + "Missing UserType(string),";

            // Password set and is valid(sufficiently complex) ?        **** Lägg till anrop till global lösenordchecker
            if (user.Password.Trim() == "") errMsg = errMsg + "Missing password or it's not complex enought,";

            return errMsg;
        }


    }
}
