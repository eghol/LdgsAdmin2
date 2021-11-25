using System.Collections.Generic;

namespace LdgsAdminAPI
{
    public class GetCustomerListResponse
    {
        //public string Customer { get; set; } = "";
        //public int CustomerId { get; set; } = 0;
        public int cntItems { get; set; } = 0;
        public GetCustomerListResponse()
        {
            Users = new List<User>();
        }
        public List<User> Users { get; set; }
    }


}