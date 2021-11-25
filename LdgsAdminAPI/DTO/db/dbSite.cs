using System;
using System.Collections.Generic;

#nullable disable

namespace LdgsAdminAPI.DTO.db
{
    public partial class dbSite
    {
        public dbSite()
        {
            CustomerSites = new HashSet<dbCustomerSite>();
            Events = new HashSet<dbEvent>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<dbCustomerSite> CustomerSites { get; set; }
        public virtual ICollection<dbEvent> Events { get; set; }
    }
}
