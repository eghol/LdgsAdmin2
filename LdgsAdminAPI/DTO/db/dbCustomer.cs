using System;
using System.Collections.Generic;

#nullable disable

namespace LdgsAdminAPI.DTO.db
{
    public partial class dbCustomer
    {
        public dbCustomer()
        {
            CustomerSites = new HashSet<dbCustomerSite>();
            CustomerUsers = new HashSet<dbCustomerUser>();
            Events = new HashSet<dbEvent>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<dbCustomerSite> CustomerSites { get; set; }
        public virtual ICollection<dbCustomerUser> CustomerUsers { get; set; }
        public virtual ICollection<dbEvent> Events { get; set; }
    }
}
