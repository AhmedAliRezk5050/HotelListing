namespace HotelListing.Utility
{
    public static class Roles
    {
        public const string User = "User";

        public const string Admin = "Admin";

        public static string Normalize(string value) => value.ToUpper(); 
    }
}
