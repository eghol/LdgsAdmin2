using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace LdgsAdminAPI.DTO.db
{
    public partial class dbUser
    {
        public dbUser()
        {
            CustomerUsers = new HashSet<dbCustomerUser>();
            Permissions = new HashSet<dbPermission>();
            Salts = new HashSet<dbSalt>();
        }

        public virtual int Id { get; set; }
        [Required]
        [StringLength(100)]
        public string Username { get; set; }
        [Required]
        [StringLength(20,MinimumLength = 8)] // max 20 tkn o minst 8
        public string Password { get; set; }        
        public string Description { get; set; }
        //[Range(1,55)] värdet måste ligga mellan 1-55
        public int UserTypeId { get; set; }       


        public virtual dbUserType UserType { get; set; }
        public virtual ICollection<dbCustomerUser> CustomerUsers { get; set; }
        public virtual ICollection<dbPermission> Permissions { get; set; }
        public virtual ICollection<dbSalt> Salts { get; set; }
    }
}
