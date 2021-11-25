using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using System.Runtime.Serialization;
using LdgsAdminAPI.Models;
namespace LdgsAdminAPI
{

    public class UserSpecific
    {
        public UserSpecific()
        {
            UserType = new UserTypes();

        }
        public int UserId { get; set; } = 0;
        public string Username { get; set; } = "";
        public string Password { get; set; } = "";
        public string Description { get; set; } = "";
        public int CustomerConnections { get; set; } = 0;
        public UserTypes UserType { get; set; }
    }

    [DataContract]
    public class User : UserSpecific
    {
        
        public  User() 
        {            
            Customers = new HashSet<Customer>();
                   
        }


        [DataMember(Name = "Customers", EmitDefaultValue = false)]        
        public virtual HashSet<Customer> Customers { get; set; }                


        //public User Clone(User obj)
        //{
        //    return new User
        //    {
        //        UserId = obj.UserId,
        //        Username = obj.Username,
        //        Description = obj.Description,
        //        Password = obj.Password,
        //        UserType = obj.UserType,
        //        Customers = obj.Customers,                                               
        //    };
        //}

    
    }
    
}
