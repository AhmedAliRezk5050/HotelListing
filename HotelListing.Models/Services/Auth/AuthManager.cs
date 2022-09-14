using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using HotelListing.Models.DataTypes;
using HotelListing.Models.DTOs.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace HotelListing.Models.Services.Auth;

public class AuthManager : IAuthManager
{
    private readonly UserManager<AppUser> _userManager;
    private readonly IConfiguration _configuration;
    private AppUser? _user;

    public AuthManager(UserManager<AppUser> userManager, IConfiguration configuration)
    {
        _userManager = userManager;
        _configuration = configuration;
    }

    public async Task<CreateTokenResponse> CreateToken()
    {
        var signInCredentials = GetSignInCredentials();
        var claims = await GetClaims();
        var token = GenerateTokenOptions(signInCredentials, claims);
        return new CreateTokenResponse
        {
            UtcValidTo = token.ValidTo,
            Token = new JwtSecurityTokenHandler().WriteToken(token)
        };
    }

    private SigningCredentials GetSignInCredentials()
    {
        var key = Environment.GetEnvironmentVariable("HotelListingApiSecretKey");
        if (key is null)
        {
            throw new Exception("No secret key found");
        }

        var secret = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));

        return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
    }
    
    private async Task<List<Claim>> GetClaims()
    {
        var claims = new List<Claim>()
        {
            new Claim(ClaimTypes.Name, _user!.UserName),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        var roles = await _userManager.GetRolesAsync(_user);

        foreach (var role in roles)
        {
            claims.Add(new Claim(ClaimTypes.Role, role));
        }

        return claims;
    }

    private JwtSecurityToken GenerateTokenOptions(SigningCredentials signInCredentials, List<Claim> claims)
    {
        var issuer = _configuration["Jwt:Issuer"];
        var audience = _configuration["Jwt:Audience"];
        var expires = DateTime.Now.AddMinutes(double.Parse(_configuration["Jwt:LifeTime"]));   
        var token = new JwtSecurityToken(
            issuer: issuer,
            claims: claims,
            expires: expires,
            signingCredentials: signInCredentials,
            audience: audience
            );

        return token;
    }

    public async Task<bool> ValidateUser(LogInUserDto logInUserDto)
    {
        _user = await _userManager.FindByNameAsync(logInUserDto.UserName);

        return _user != null && await _userManager.CheckPasswordAsync(_user, logInUserDto.Password);
    }
}
