using HotelListing.Models.DataTypes;
using HotelListing.Models.DTOs.User;
using Microsoft.IdentityModel.Tokens;

namespace HotelListing.Models.Services.Auth;

public interface IAuthManager
{
    Task<bool> ValidateUser(LogInUserDto logInUserDto);
    Task<CreateTokenResponse> CreateToken();
}