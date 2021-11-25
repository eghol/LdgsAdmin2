using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LdgsAdminAPI.DTO.db;
using AutoMapper;

using System.Reflection;

namespace LdgsAdminAPI.DTO.request
{
    public class reqUser 
    {        
        public reqUser()
        {
          
        }
        public string Username { get; set; } = "";
        public string Password { get; set; } = "";
        public string Description { get; set; } = "";
        public string UserType { get; set; } = "";
        public virtual int UserTypeId { get; set; } = 0;
        public virtual IList<string> Permissions { get; set; }
    }        
}

    

