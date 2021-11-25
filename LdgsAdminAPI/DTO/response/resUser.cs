using System;
using System.Collections.Generic;
using LdgsAdminAPI.DTO.db;
using System.Linq;
using System.Threading.Tasks;

namespace LdgsAdminAPI.DTO.response
{
    public class resUser
    {

        public virtual int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Description { get; set; }        
        public string UserType { get; set; }
        public int UserTypeID { get; set; }


        //public virtual dbUserType UserType { get; set; }
        //public virtual ICollection<dbCustomerUser> CustomerUsers { get; set; }
        public List<dbPermission> Permissions { get; set; }
        //public virtual ICollection<dbSalt> Salts { get; set; }

    }
}
