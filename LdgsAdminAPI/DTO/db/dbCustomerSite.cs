using System;
using System.Collections.Generic;

#nullable disable

namespace LdgsAdminAPI.DTO.db
{
    public partial class dbCustomerSite
    {
        public dbCustomerSite()
        {
            Configurations = new HashSet<dbConfiguration>();
        }

        public int Id { get; set; }
        public int SiteId { get; set; }
        public int CustomerId { get; set; }

        public virtual dbCustomer Customer { get; set; }
        public virtual dbSite Site { get; set; }
        public virtual ICollection<dbConfiguration> Configurations { get; set; }
    }
}
