using AutoMapper;
using HotelListing.Models.DTOs.Country;
using HotelListing.Models.DTOs.Hotel;

namespace HotelListing.Models.AutoMapper;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Country, CountryDTO>()
            .ReverseMap();
        CreateMap<Country, CreateCountryDTO>()
            .ReverseMap();

        CreateMap<Hotel, HotelDTO>()
            .ReverseMap();
        CreateMap<Hotel, CreateHotelDTO>()
            .ReverseMap();
    }
}