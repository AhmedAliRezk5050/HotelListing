using Microsoft.AspNetCore.Identity;

namespace HotelListing.Models;

public class AppUser : IdentityUser
{
    public string? FirsName { get; set; } = null!;

    public string? LastName { get; set; } = null!;
}