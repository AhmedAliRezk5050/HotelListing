using AutoMapper;
using HotelListing.Models.DTOs.Country;
using HotelListing.Models.DTOs.Hotel;
using HotelListing.Models.DTOs.User;

namespace HotelListing.Models.AutoMapper;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Country, CountryDto>()
            .ReverseMap();
        CreateMap<Country, CreateCountryDto>()
            .ReverseMap();
        CreateMap<Country, UpdateCountryDto>()
            .ReverseMap();

        CreateMap<Hotel, HotelDto>()
            .ReverseMap();
        CreateMap<Hotel, CreateHotelDto>()
            .ReverseMap();
        CreateMap<Hotel, UpdateHotelDto>()
            .ReverseMap();
        
        CreateMap<AppUser, CreateUserDto>()
            .ReverseMap();
        
      
    }
}