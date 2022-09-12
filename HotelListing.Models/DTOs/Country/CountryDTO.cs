using HotelListing.Models.Dtos.Hotel;

namespace HotelListing.Models.Dtos.Country;

public class CountryDto: CreateCountryDto
{
    public int Id { get; set; }

    public List<HotelDto> Hotels { get; set; } = null!;
}