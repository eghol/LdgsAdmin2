using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Text;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using LdgsAdminAPI.Models;
using LdgsAdminAPI.Interface;
namespace LdgsAdminAPI.Data
{
    public class AdminDbHelper : IAdminDbHelper
    {
        private readonly ILogger<AdminDbHelper> _logger;
        private readonly IConfiguration _config;
        private readonly string cns;
        private SqlConnection connection = new ();
        private SqlCommand command = new ();
        private SqlParameter SqlParam = new ();
        
        private IEnumerable<User> table_Users = new List<User>();
        private IEnumerable<Customer> table_Customers = new List<Customer>();
        private IEnumerable<UserTypes> table_UserTypes = new List<UserTypes>();
        private List<CustomerAndUser> table_CustomerAndUsers = new List<CustomerAndUser>();        
        public AdminDbHelper(ILogger<AdminDbHelper> logger, IConfiguration config)
        {
            _config = config;
            _logger = logger;            
            cns = config.GetConnectionString("LdgsAdminAPIContext");
        }


        // -----------------------------------------------------------------------------------------------------------------------------------------------------    Main-methods (for external calls)

        public bool ConnectToDb(string ConnectionString = "", bool forceNewConnection = false)
        {
            if (!forceNewConnection && connection.State == ConnectionState.Open) return true;

            if (forceNewConnection && connection.State == ConnectionState.Open) connection.Close();

            if (string.IsNullOrEmpty(ConnectionString)) return false;

            connection = new SqlConnection(ConnectionString);
            try
            {
                connection.Open();
                return true;
            }
            catch
            {
                _logger.LogInformation($"ConnectToDb : failed, ConnectionString = {cns}");
                return false;
            }

        }

        public async Task<SqlDataReader> getDataFromDB(string SQL)
        {
            if (!ConnectToDb(cns)) return null;
            try
            {
                command = new SqlCommand(SQL, connection);
                SqlDataReader reader = await command.ExecuteReaderAsync();
                return reader;
            }
            catch (Exception e)
            {
                {
                    _logger.LogInformation($"ConnectToDb : failed, ConnectionString = {cns} -> {e.Message}");
                }
                return null;
            }
        }


        public async Task<GetUserListResponse> ListAllUser()
        {
            var data = await getAllUser(); 


            return data;

        }

        // selects and returns all Customers who belong to given UserID
        public async Task<GetUserResponse> ListUserToCustomerID(int CustomerID)
        {

            var data = await getUserOfCustomerID(CustomerID);
            return data;

        }
        
        public async Task AddUser(AddUserRequest request)
        {
            //db-kod för att lägga till användare
           
        }



        // -----------------------------------------------------------------------------------------------------------------------------------------------------    Mostly Internal methods



             
        private async Task<bool> initAllRegister(int UserID = 0, int CustomerID = 0)
        {
            try {

                table_Users = table_Users.Where(e => e.UserId < 0);
                table_Users = await db_Users();

                table_UserTypes = table_UserTypes.Where(e => e.Id < 0);
                table_UserTypes = await db_UserTypes();

                table_Customers = table_Customers.Where(e => e.CustomerId < 0);
                table_Customers = await db_Customers();
                             
                table_CustomerAndUsers.Clear();
                await db_CustomerAndUsers();
                return true;

            } catch (Exception e)
            {
                _logger.LogInformation($"initTable_User_setPropUserTypeName failed : ConnectionString = {cns} " + e.Message);
                return false;
            }
        }
        


        /* Init neaccesery register( from db: tables), they are global for better performance 
         * Param : userID(optional) If UserID is set -> limits fetch of data to just one specific user
        */
        public async Task<GetUserListResponse> getAllUser()
        {
            try
            {                
                bool succed = await initAllRegister();
                if (!succed) return null;

                GetUserListResponse data = new ();
                List<Customer> newCustomer = new ();
                

                List<int> distinctUsers = table_CustomerAndUsers.GroupBy(g => (g.UserId, g.Username)).Select(s => s.Key.UserId).ToList<int>();

                foreach (int curUserID in distinctUsers)
                {
                    CustomerAndUser item = table_CustomerAndUsers.Where(u => u.UserId == curUserID).FirstOrDefault();
                    if (item != null)
                    {
                        User newUser = new();
                        newUser.UserId = curUserID;
                        newUser.Username = item.Username;
                        newUser.Password = item.Password;
                        newUser.Description = item.Description;
                        newUser.UserType.Id = item.TypeId;
                        newUser.UserType.Name = item.TypeName;
                        
                        table_CustomerAndUsers.Where(c => c.UserId == curUserID).ToList().ForEach(z => {

                            Customer newCustomer = new();
                            newUser.Customers.Add(new Customer() { Name = z.CustomerName, CustomerId = z.CustomerId });
                        });
                        
                        data.Users.Add(newUser);
                        data.cntItems++;
                    } else { 
                        _logger.LogInformation($"getAllUser(loop: foreach (int curUserID in distinctUsers)) failed on UserId = {curUserID}");
                    }
                }            

                return data;

            }
            catch (Exception e)
            {
                _logger.LogInformation($"getAllUser() : Failed -> " + e.Message);
                return null;
            }
        }
        /* Init neaccesery register( from db: tables), they are global for better performance 
        * Param : userID(optional) If UserID is set -> limits fetch of data to just one specific user
       */
        public async Task<GetUserResponse> getUserOfCustomerID(int CustomerID)
        {
            try
            {
                bool succed = await initAllRegister(0, CustomerID); // enbart poster ur CustomerUsers som hör till aktuell Customer
                
                if (!succed) return null;

                GetUserResponse data = new();                

                List<int> distinctUsers = table_CustomerAndUsers.GroupBy(g => (g.UserId, g.Username)).Select(s => s.Key.UserId).ToList<int>();

                foreach (int curUserID in distinctUsers)
                {
                    CustomerAndUser item = table_CustomerAndUsers.Where(u => u.UserId == curUserID && u.CustomerId == CustomerID).FirstOrDefault();
                    if (item != null)
                    {
                        User newUser = new();
                        newUser.UserId = curUserID;
                        newUser.Username = item.Username;
                        newUser.Password = item.Password;
                        newUser.Description = item.Description;
                        newUser.UserType.Id = item.TypeId;
                        newUser.UserType.Name = item.TypeName;


                        table_CustomerAndUsers.Where(c => c.UserId == curUserID).ToList().ForEach(z => {

                            Customer newCustomer = new();
                            newUser.Customers.Add(new Customer() { Name = z.CustomerName, CustomerId = z.CustomerId });
                        });

                        int cntCustCon = 0;
                        cntCustCon = table_CustomerAndUsers.Where(c => c.UserId == curUserID).Count();
                            
                        newUser.CustomerConnections = cntCustCon;                        
                        data.Users.Add(newUser);
                        data.cntItems++;
                    }
                    else
                    {
                        _logger.LogInformation($"UserList(loop: foreach (int curUserID in distinctUsers)) failed on CustomerID = {CustomerID} UserId = {curUserID}");
                    }
                }
                return data;           

            }
            catch (Exception e)
            {
                
                _logger.LogInformation($"UserList() : Failed -> UserID : {CustomerID} \n" + e.Message);
                return null;
            }
        }




        //_________________________________________________                    Methods poplute data from sorucetables : User CustomerUsers,Customers, UserTypes to corresponding into <list>-object table_X (X = corresponding table-name)

        private async Task<SqlDataReader> getReader_CustomerAndUsers(int UserID = 0, int CustomerID = 0)
        {
            SqlDataReader reader;
            if (UserID == 0 && CustomerID == 0)
            {
                reader = await getDataFromDB("SELECT * FROM CustomerUsers ORDER BY Id");
            }
            else if (UserID == 0 && CustomerID > 0)
            {
                reader = await getDataFromDB($"SELECT * FROM CustomerUsers where CustomerID={CustomerID} ORDER BY CustomerID");
            }
            else if (UserID > 0 && CustomerID == 0)
            {
                reader = await getDataFromDB($"SELECT * FROM CustomerUsers where UserID= {UserID} ORDER BY UserID");
            }
            else
            {
                reader = await getDataFromDB($"SELECT * FROM CustomerUsers where UserID= {UserID} AND CustomerID={CustomerID} ORDER BY UserID");
            }
            return reader;
        }

        // Get All data from DB: Sourcetable db_CustomerUsers
        public async Task db_CustomerAndUsers(int UserID = 0, int CustomerID = 0)
        {
            SqlDataReader reader = await getReader_CustomerAndUsers(UserID, CustomerID);

            try
            {
                while (reader.Read())
                {
                    CustomerAndUser CandU = new();
                    CandU.CustomerId = reader.GetInt32("CustomerId");
                    List<Customer> c = table_Customers.Where(e => e.CustomerId == CandU.CustomerId).ToList();//.FirstOrDefault().Name;

                    if (c.Count() != 0)
                    {
                        CandU.CustomerName = c.FirstOrDefault().Name != null ? c.FirstOrDefault().Name : "";
                    
                        CandU.UserId = reader.GetInt32("UserId");

                        var curUser = table_Users.Where(e => e.UserId == CandU.UserId).FirstOrDefault();
                        if (curUser != null)
                        {
                            CandU.Username = curUser.Username;
                            CandU.Password = curUser.Password;
                            CandU.Description = curUser.Description;
                            CandU.Password = curUser.Password;
                            CandU.TypeId = curUser.UserType.Id;
                            CandU.TypeName = table_UserTypes.Where(e => e.Id == curUser.UserType.Id).FirstOrDefault().Name;
                        }

                        table_CustomerAndUsers.Add(CandU);
                    }
                    else { 
                     //int d = 1;
                    }
                }           
            } 
            catch(Exception e) 
            {                
                    _logger.LogInformation($"db_CustomerAndUsers() failed.  In-param: UserID = {UserID},  CustomerID = {CustomerID} -> " + e.Message);                               
            }


            
        }


        // Get all data from DB Sourcetable Customers
        public async Task<IEnumerable<Customer>> db_Customers(int CustomerID = 0)  
        {
            SqlDataReader reader;
            List<Customer> data = new();
            if (CustomerID < 1)
            {
                reader = await getDataFromDB("SELECT * FROM Customers ORDER BY Id");
            } else
            {
                reader = await getDataFromDB($"SELECT * FROM Customers where Id = {CustomerID} ORDER BY Id");
            } 
            
            while (reader.Read())
            {
                Customer ut = new();
                ut.CustomerId = reader.GetInt32("Id");
                ut.Name = reader.GetString("Name");
                data.Add(ut);
            }
            
            return data;
        }


        // Get all data from DB Sourcetable  UserTypes
        public async Task<IEnumerable<UserTypes>> db_UserTypes()  
        {            
            List<UserTypes> data = new();
            SqlDataReader reader = await getDataFromDB("SELECT * FROM UserTypes ORDER BY Id");            
            
            while (reader.Read())
            {
                UserTypes ut = new();
                ut.Id = reader.GetInt32("ID");
                ut.Name = reader.GetString("Name");
                data.Add(ut);
            }

            return data;
        }


        // Get all data from DB Sourcetable  Users
        public async Task<IEnumerable<User>> db_Users(int UserID = 0) 
        {            
            List<User> data = new();
            SqlDataReader reader;
            if (UserID < 1)
            {
                reader = await getDataFromDB("SELECT * FROM Users ORDER BY Id");
            }
            else
            {
                reader = await getDataFromDB($"SELECT * FROM Users WHERE Id = {UserID} ORDER BY Id");
            }


            while (reader.Read())
            {                
                User obj = new();                                    
                obj.UserId = reader.GetInt32("Id");  
                obj.Username = reader.GetString("Username");
                obj.Password = reader.GetString("Password");
                obj.Description = reader.GetString("Description");
                obj.UserType.Id = reader.GetInt32("UserTypeId");                
                data.Add(obj);
            }

            return data;
        }


      

    }
}
