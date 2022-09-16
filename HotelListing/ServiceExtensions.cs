using System.Text;
using HotelListing.Models;
using HotelListing.Models.Services.Auth;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.IdentityModel.Tokens;
using Serilog;

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

    public static void ConfigureExceptionHandler(this IApplicationBuilder app)
    {
        app.UseExceptionHandler(error =>
        {
            error.Run(async context =>
            {
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                context.Response.ContentType = "application/json";

                var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                if (contextFeature is not null)
                {
                    Log.Error("Something went wrong: {ContextFeatureError}",
                        contextFeature.Error.Message);
                    await context.Response.WriteAsync(new Error()
                    {
                        StatusCode = StatusCodes.Status500InternalServerError,
                        Message = "Internal server error. please try again later."
                    }.ToString());
                }
            });
        });
    }

    public static void ConfigureVersioning(this IServiceCollection services)
    {
        services.AddApiVersioning(opt =>
        {
            opt.ReportApiVersions = true;
            opt.AssumeDefaultVersionWhenUnspecified = true;
            opt.DefaultApiVersion = new ApiVersion(1, 0);
            opt.ApiVersionReader = new HeaderApiVersionReader("api-version");
        });
    }
    
    private static SymmetricSecurityKey GetSecurityKey()
    {
        var key = Environment.GetEnvironmentVariable("HotelListingApiSecretKey");
        if (key is null)
        {
            throw new Exception("No secret key found");
        }

        return new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
    }
}