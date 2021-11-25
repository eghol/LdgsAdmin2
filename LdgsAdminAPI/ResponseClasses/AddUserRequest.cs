using System.Collections.Generic;

namespace LdgsAdminAPI
{
    public class AddUserRequest : User
    {
        public new List<int> CustomerConnections { get; set; } // Finns denna skapa koppling

    }
}