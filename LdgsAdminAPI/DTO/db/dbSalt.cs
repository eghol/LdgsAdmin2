using System;
using System.Collections.Generic;

#nullable disable

namespace LdgsAdminAPI.DTO.db
{
    public partial class dbSalt
    {
        public int Id { get; set; }
        public string Salt1 { get; set; }
        public int UserId { get; set; }

        public virtual dbUser User { get; set; }
    }
}
