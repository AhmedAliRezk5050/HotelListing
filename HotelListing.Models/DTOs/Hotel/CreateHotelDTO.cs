using System.ComponentModel.DataAnnotations;

namespace HotelListing.Models.DTOs.Hotel;

public class CreateHotelDTO
{
    [MaxLength(50, ErrorMessage = "Hotel name is too long(Max: {1})")]
    public string Name { get; set; } = null!;

    [MaxLength(50, ErrorMessage = "Hotel Address is too long(Max: {1})")]
    public string Address { get; set; } = null!;

    [Required(ErrorMessage = "Hotel rating is required")]
    [Range(1, 5, ErrorMessage = "Hotel rating must be from {1} to {2}")]
    public decimal? Rating { get; set; }

    [Required(ErrorMessage = "Country is required")]
    public int? CountryId { get; set; }
}