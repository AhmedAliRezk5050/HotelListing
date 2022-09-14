using HotelListing.Models.DTOs.Hotel;

namespace HotelListing.Models.DTOs.Country;

public class CountryDto: CreateCountryDto
{
    public int Id { get; set; }

    public List<HotelDto> Hotels { get; set; } = null!;
}