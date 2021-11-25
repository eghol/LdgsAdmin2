using System.Collections.Generic;
using System.Xml.Serialization;
using System.Text.Json.Serialization;
using System.Runtime.Serialization;

namespace LdgsAdminAPI
{    
    public class Customer
    {
        public Customer()
        {
           Users = new HashSet<User>();
        }
        public int CustomerId { get; set; } = 0;
        public string Name { get; set; } = "";
         
        public virtual HashSet<User> Users { get; set; }

    }
}