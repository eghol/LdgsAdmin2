using System;
using System.Collections.Generic;

#nullable disable

namespace LdgsAdminAPI.DTO.db
{
    public partial class dbConfiguration
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
        public int? CustomerSiteId { get; set; }

        public virtual dbCustomerSite CustomerSite { get; set; }
    }
}
