using HotelListing.DataAccess;
using Microsoft.EntityFrameworkCore;
using Serilog;

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

            builder.Host.UseSerilog((ctx, lc) => lc
                .WriteTo.Console());
            builder.Services.AddControllers();

            builder.Services.AddCors(o =>
            {
                o.AddPolicy("AllowAll",
                    policyBuilder =>
                        policyBuilder.AllowAnyOrigin()
                            .AllowAnyMethod()
                            .AllowAnyHeader());
            });


            builder.Services.AddDbContext<DataContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("HotelListingConnection"));
                options.EnableSensitiveDataLogging(true);
            }
       );


            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            app.UseSwagger();
            app.UseSwaggerUI();

            app.UseHttpsRedirection();

            app.UseCors("AllowAll");
            
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