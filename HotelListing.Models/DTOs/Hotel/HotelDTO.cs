using HotelListing.Models.Dtos.Country;

namespace HotelListing.Models.Dtos.Hotel;

public class HotelDto: CreateHotelDto
{
    public int Id { get; set; }

    public CountryDto CountryDto{ get; set; } = null!;
}