using System;
using System.Collections.Generic;

#nullable disable

namespace LdgsAdminAPI.DTO.db
{
    public partial class dbCustomerUser
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public int UserId { get; set; }

        public virtual dbCustomer Customer { get; set; }
        public virtual dbUser User { get; set; }
    }
}
