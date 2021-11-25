using System;
using System.Collections.Generic;

#nullable disable

namespace LdgsAdminAPI.DTO.db
{
    public partial class dbEventProp
    {
        public int Id { get; set; }
        public int? Type { get; set; }
        public string Title { get; set; }
        public bool? Value { get; set; }
        public int? Step { get; set; }
        public int EventId { get; set; }

        public virtual dbEvent Event { get; set; }
    }
}
