using System;
using System.Collections.Generic;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelListing.Utiliy
{
    public static class Roles
    {
        public const string User = "User";

        public const string Admin = "Admin";

        public static string Normalize(string value) => value.ToUpper(); 
    }
}
