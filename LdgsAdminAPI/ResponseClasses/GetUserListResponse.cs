using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using System.Xml.Serialization;
namespace LdgsAdminAPI
{
    
    public class GetUserListResponse
    {
        public int cntItems { get; set; }= 0;
        public List<User> Users { get; set; }
        public GetUserListResponse()
        {
            Users = new List<User>();
        }

    }


}