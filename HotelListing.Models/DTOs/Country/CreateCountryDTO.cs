using System.ComponentModel.DataAnnotations;

namespace HotelListing.Models.DTOs.Country;

public class CreateCountryDTO
{
    [MaxLength(50, ErrorMessage = "Country name is too long(Max: {1})")]
    public string Name { get; set; } = null!;

    [MaxLength(2, ErrorMessage = "Country short name is too long(Max: {1})")]
    public string ShortName { get; set; } = null!;
}