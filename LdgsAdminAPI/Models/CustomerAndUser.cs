using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LdgsAdminAPI.Models
{
    public class CustomerAndUser
    {
        public int CustomerId { get; set; } = 0;
        public string CustomerName { get; set; } = "";
        public int UserId { get; set; } = 0;
        public string Username { get; set; } = "";
        public string Password { get; set; } = "";
        public string Description { get; set; } = "";
        public int TypeId { get; set; } = 0;
        public string TypeName { get; set; } = "";
        public bool processed { get; set; } = false;
        public int CustomerConnections { get; set; } = 0;
    }
}
