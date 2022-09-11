using HotelListing.Models.DTOs.Country;

namespace HotelListing.Models.DTOs.Hotel;

public class HotelDTO : CreateHotelDTO
{
    public int Id { get; set; }

    public CountryDTO Country { get; set; } = null!;
}