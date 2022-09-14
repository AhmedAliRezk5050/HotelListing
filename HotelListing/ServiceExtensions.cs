using System.Text;
using HotelListing.Models.Services.Auth;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace HotelListing;

public static class ServiceExtensions
{
    public static void ConfigureJwt(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddAuthentication(o =>
            {
                o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                o.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(o =>
            {
                o.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = configuration["Jwt:Issuer"],
                    ValidAudience = configuration["Jwt:Audience"],
                    IssuerSigningKey = GetSecurityKey(),
                    ValidateAudience = true
                };
            });
    }
    
    private static  SymmetricSecurityKey GetSecurityKey()
    {
        var key = Environment.GetEnvironmentVariable("HotelListingApiSecretKey");
        if (key is null)
        {
            throw new Exception("No secret key found");
        }
        return new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
    }
}