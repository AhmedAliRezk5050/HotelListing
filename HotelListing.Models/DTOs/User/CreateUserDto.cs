using HotelListing.Models.CustomAttributes;
using HotelListing.Models.DTOs.User;
using System.ComponentModel.DataAnnotations;

namespace HotelListing.Models.Dtos.User;

public class CreateUserDto : LogInUserDto
{
    public string? FirstName { get; set; }
    
    public string? LastName { get; set; }
    
    [DataType(DataType.PhoneNumber)]
    public string? PhoneNumber { get; set; }

    [MustHaveOneElement]
    public List<string>? Roles { get; set; }

}