using HotelListing.Models.DTOs.Hotel;

namespace HotelListing.Models.DTOs.Country;

public class CountryDTO : CreateCountryDTO
{
    public int Id { get; set; }

    public List<HotelDTO> Hotels { get; set; }
}