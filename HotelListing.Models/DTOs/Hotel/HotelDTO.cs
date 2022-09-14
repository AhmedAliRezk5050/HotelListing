using HotelListing.Models.DTOs.Country;

namespace HotelListing.Models.DTOs.Hotel;

public class HotelDto: CreateHotelDto
{
    public int Id { get; set; }

    public CountryDto CountryDto{ get; set; } = null!;
}