using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using LdgsAdminAPI.Models;
namespace LdgsAdminAPI.Interface
{
    public interface IAdminDbHelper
    {
        public Task AddUser(AddUserRequest request);        

        public Task<IEnumerable<UserTypes>> db_UserTypes();
        
        public Task<IEnumerable<User>> db_Users(int UserID = 0);

        public Task<IEnumerable<Customer>> db_Customers(int CustomerID = 0);

        public Task<GetUserResponse> getUserOfCustomerID(int CustomerID);
        public Task<GetUserResponse> ListUserToCustomerID(int CustomerID);        
        public Task<GetUserListResponse> ListAllUser();


    }
}
