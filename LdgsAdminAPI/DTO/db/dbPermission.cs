using System;
using System.Collections.Generic;

#nullable disable

namespace LdgsAdminAPI.DTO.db
{
    public partial class dbPermission
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int PermissionTypeId { get; set; }

        public virtual dbPermissionType PermissionType { get; set; }
        public virtual dbUser User { get; set; }
    }
}
