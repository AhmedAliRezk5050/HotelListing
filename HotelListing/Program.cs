using System.Text.Json.Serialization;
using HotelListing.DataAccess;
using HotelListing.DataAccess.IRepository;
using HotelListing.DataAccess.Repository;
using HotelListing.Models;
using HotelListing.Models.AutoMapper;
using HotelListing.Models.Services.Auth;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Serilog;
using ILogger = Microsoft.Extensions.Logging.ILogger;

namespace HotelListing
{
    public class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                ConfigureLog();
                BuildApp(args);
            }
            catch (Exception ex)
            {
                string type = ex.GetType().Name;
                if (type.Equals("StopTheHostException", StringComparison.Ordinal))
                {
                    throw;
                }

                Log.Fatal(ex, "Application failed to start");
            }
            finally
            {
                Log.Information("Shut down complete");
                Log.CloseAndFlush();
            }
        }

        private static void BuildApp(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var configuration = builder.Configuration;
            builder.Host.UseSerilog((ctx, lc) => lc
                .WriteTo.Console());

            builder.Services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
                options.JsonSerializerOptions.WriteIndented = true;
            });
            ;

            builder.Services.AddCors(o =>
            {
                o.AddPolicy("AllowAll",
                    policyBuilder =>
                        policyBuilder.AllowAnyOrigin()
                            .AllowAnyMethod()
                            .AllowAnyHeader());
            });

            builder.Services.AddAutoMapper(typeof(MappingProfile));

            builder.Services.AddDbContext<DataContext>(options =>
                {
                    options.UseSqlServer(builder.Configuration.GetConnectionString("HotelListingConnection"));
                    options.EnableSensitiveDataLogging(true);
                }
            );

            builder.Services.AddIdentity<AppUser, IdentityRole>(
                    options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<DataContext>()
                .AddDefaultTokenProviders();
            

            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            
            builder.Services.AddScoped<IAuthManager, AuthManager>();
            
            builder.Services.ConfigureJwt(configuration);

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();
           
            app.UseSwagger();
            app.UseSwaggerUI();


            app.UseCors("AllowAll");
            
            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }

        private static void ConfigureLog()
        {
            Log.Logger = new LoggerConfiguration()
                .WriteTo.Console()
                .CreateBootstrapLogger();

            Log.Information("Starting up");
        }
    }
}