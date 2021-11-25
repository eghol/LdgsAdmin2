//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.Extensions.Logging;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using LdgsAdminAPI.DbContext;
//using LdgsAdminAPI.Models;
//using System.Data;
//using MVCFiltersDemo.ActionFilters;

//namespace LdgsAdminAPI.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class UserController : ControllerBase
//    {
//        private ILogger<UserController> _logger;
//        private IAdminDbHelper _helper;
//        private DataTable data = new DataTable();
//        public UserController(ILogger<UserController> logger, IAdminDbHelper helper)
//        {
//            _logger = logger;
//            _helper = helper;
//        }



//        //{{baseURL}}/User/
//        //{{baseURL}}/User/?customerId=1
//        [HttpGet("CustomerID")]        
//        [Test]
//        public async Task<GetUserResponse> GetUserList([FromQuery] int customerId = 0)
//        //public async Task<GetUserResponse> GetUserList([FromQuery] int customerId = 0)
//        {
//                GetUserResponse data = await _helper.ListUserToCustomerID(customerId);
//                return data;
//        }


//        [HttpGet()]
//        public async Task<GetUserListResponse> GetUserList()
//        {
//            GetUserListResponse data = await _helper.ListAllUser();
//            return data;
//        }







//        [HttpGet("PwdToSaltTest")]
//        [ActionName("PwdToSaltTest")]
//        public async Task<List<string>> PwdToSaltTest(string Password)
//        {
//            LdgsAdminAPI.PasswordGenerator pwdGen = new LdgsAdminAPI.PasswordGenerator();
//            string hashedPwd = pwdGen.HashPassword(Password);
//            bool verifyed = pwdGen.VerifyHashedPassword(hashedPwd, Password);
//            List<string> answer = new List<string>();
//            answer.Add("Password :" + Password);
//            answer.Add("Hashed password :" + hashedPwd);
//            answer.Add("Password vierfyed :" + verifyed);
//            return answer;
//        }




//        [HttpPost()]
//        public async Task<ActionResult> AddUser([FromBody] AddUserRequest request)
//        {
//           // _helper.AddUser(request);
//           // _helper.ConnectUserToCustomer(request);

//            return Ok();
//        }
//    }
//}
