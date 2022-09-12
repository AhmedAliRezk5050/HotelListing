using AutoMapper;
using HotelListing.Models.Dtos.Country;
using HotelListing.Models.Dtos.Hotel;
using HotelListing.Models.Dtos.User;

namespace HotelListing.Models.AutoMapper;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Country, CountryDto>()
            .ReverseMap();
        CreateMap<Country, CreateCountryDto>()
            .ReverseMap();

        CreateMap<Hotel, HotelDto>()
            .ReverseMap();
        CreateMap<Hotel, CreateHotelDto>()
            .ReverseMap();
        
        CreateMap<AppUser, CreateUserDto>()
            .ReverseMap();
    }
}