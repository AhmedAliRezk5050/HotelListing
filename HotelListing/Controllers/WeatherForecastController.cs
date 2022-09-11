using HotelListing.DataAccess;
using HotelListing.DataAccess.IRepository;
using HotelListing.Models;
using Microsoft.AspNetCore.Mvc;

namespace HotelListing.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly IUnitOfWork _unitOfWork;

        private readonly DataContext _context;
        
        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IUnitOfWork unitOfWork, DataContext context)
        {
            _logger = logger;

            _unitOfWork = unitOfWork;
            
            _context =context
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public async Task<IEnumerable<Country>> Get()
        {
            return await _unitOfWork.Countries.GetAllAsync();
        }
    }
}