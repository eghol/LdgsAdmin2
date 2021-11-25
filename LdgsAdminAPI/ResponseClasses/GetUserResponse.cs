

using System.Collections.Generic;
using LdgsAdminAPI.Models;
namespace LdgsAdminAPI
{
    public class GetUserResponse 
    {
        public int cntItems { get; set; } = 0;
        public GetUserResponse()
        {
            Users =new HashSet<UserSpecific>();
        }
        public virtual HashSet<UserSpecific> Users { get; set; } = new HashSet<UserSpecific>();
    }
}