using System;
using System.Collections.Generic;

#nullable disable

namespace LdgsAdminAPI.DTO.db
{
    public partial class dbUserType
    {
        public dbUserType()
        {
            Users = new HashSet<dbUser>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<dbUser> Users { get; set; }
    }
}
