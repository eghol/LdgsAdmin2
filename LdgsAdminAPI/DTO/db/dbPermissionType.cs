using System;
using System.Collections.Generic;

#nullable disable

namespace LdgsAdminAPI.DTO.db
{
    public partial class dbPermissionType
    {
        public dbPermissionType()
        {
            Permissions = new HashSet<dbPermission>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<dbPermission> Permissions { get; set; }
    }
}
