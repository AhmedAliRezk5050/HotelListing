using AutoMapper;
using HotelListing.DataAccess;
using HotelListing.DataAccess.IRepository;
using HotelListing.Models;
using HotelListing.Models.DTOs.Country;
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

        private readonly IMapper _mapper;
        
        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(
            ILogger<WeatherForecastController> logger,
            IUnitOfWork unitOfWork,
            IMapper mapper
            )
        {
            _logger = logger;

            _unitOfWork = unitOfWork;

            _mapper = mapper;

        }

        [HttpGet(Name = "GetWeatherForecast")]
        public async Task<IEnumerable<Country>> Get()
        {
            CountryDTO countryDto = _mapper.Map<CountryDTO>(new Country()
            {
                Name = "Test Name"
            });
            
            
            return await _unitOfWork.Countries.GetAllAsync();
        }
    }
}