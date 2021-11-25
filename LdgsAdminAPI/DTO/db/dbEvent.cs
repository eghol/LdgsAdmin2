using System;
using System.Collections.Generic;

#nullable disable

namespace LdgsAdminAPI.DTO.db
{
    public partial class dbEvent
    {
        public dbEvent()
        {
            EventProps = new HashSet<dbEventProp>();
        }

        public int Id { get; set; }
        public DateTime ChangeDate { get; set; }
        public string TopicId { get; set; }
        public string ErrorCode { get; set; }
        public string CaseId { get; set; }
        public string Model { get; set; }
        public string Series { get; set; }
        public DateTime? MigDate { get; set; }
        public int SiteId { get; set; }
        public int CustomerId { get; set; }

        public virtual dbCustomer Customer { get; set; }
        public virtual dbSite Site { get; set; }
        public virtual ICollection<dbEventProp> EventProps { get; set; }
    }
}
