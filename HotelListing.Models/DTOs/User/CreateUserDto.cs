using System.ComponentModel.DataAnnotations;
using HotelListing.Models.CustomAttributes;

namespace HotelListing.Models.DTOs.User;

public class CreateUserDto : LogInUserDto
{
    public string? FirstName { get; set; }
    
    public string? LastName { get; set; }
    
    [DataType(DataType.EmailAddress)]
    public string Email { get; set; } = null!;
    
    [DataType(DataType.PhoneNumber)]
    public string? PhoneNumber { get; set; }

    [MustHaveOneElement]
    public List<string>? Roles { get; set; }

}