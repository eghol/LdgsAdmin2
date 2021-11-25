using System;
using System.Collections.Generic;

#nullable disable

namespace LdgsAdminAPI.DTO.db
{
    public partial class dbAdminCustomerAndSite
    {
        public long? Id { get; set; }
        public int? CustomerId { get; set; }
        public string Customer { get; set; }
        public int? SiteId { get; set; }
        public string Site { get; set; }
        public int CustomersSitesId { get; set; }
    }
}
