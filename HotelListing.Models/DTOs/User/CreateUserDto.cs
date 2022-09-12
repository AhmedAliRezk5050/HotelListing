using System.ComponentModel.DataAnnotations;

namespace HotelListing.Models.DTOs.User;

public class CreateUserDto
{
    public string? FirstName { get; set; }
    
    public string? LastName { get; set; }

    [StringLength(15, MinimumLength = 5, ErrorMessage = "Username can be only between {2} to {1} characters")]
    public string UserName { get; set; } = null!;
    
    [DataType(DataType.PhoneNumber)]
    public string? PhoneNumber { get; set; }

    [DataType(DataType.EmailAddress)]
     public string Email { get; set; } = null!;
     
     [MinLength(6, ErrorMessage = "Password is too short. minimum length is {1}")]
     public string Password { get; set; } = null!;
}