using AutoMapper;
using HotelListing.DataAccess.IRepository;
using HotelListing.Models.DTOs.Country;
using Microsoft.AspNetCore.Mvc;

namespace HotelListing.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CountryController : ControllerBase
{
    private readonly ILogger<CountryController> _logger;

    private readonly IUnitOfWork _unitOfWork;

    private readonly IMapper _mapper;

    public CountryController(ILogger<CountryController> logger, IUnitOfWork unitOfWork, IMapper mapper)
    {
        _logger = logger;

        _unitOfWork = unitOfWork;


        _mapper = mapper;
    }

    // GET: api/Countries
    [HttpGet]
    public async Task<IActionResult> GetCountries()
    {
        try
        {
            var countries = await _unitOfWork.Countries.GetAllAsync();
            List<CountryDTO> countryDtoList = _mapper.Map<List<CountryDTO>>(countries);
            return Ok(countryDtoList);
        }
        catch (Exception e)
        {
            _logger.LogError(e, $"Error in {nameof(CountryController)} controller" +
                                $" and {nameof(GetCountries)} Action");
            return StatusCode(500, "Internal server error. Try again later");
        }
    }

    // GET: api/Countries/5
    [HttpGet("{id}")]
    public async Task<IActionResult> GetCountry(int id)
    {
        try
        {
            if (id == 0)
            {
                return NotFound();
            }

            var country = await _unitOfWork.Countries.GetAsync(c => c.Id == id, new List<string>{"Hotels"});
            CountryDTO countryDto = _mapper.Map<CountryDTO>(country);

            if (countryDto is null)
            {
                return NotFound();
            }
            {
                
            }
            
            return Ok(country);
        }
        catch (Exception e)
        {
            _logger.LogError(e, $"Error in [{nameof(CountryController)}] controller" +
                                $" and [{nameof(GetCountries)}] Action");
            return StatusCode(500, "Internal server error. Try again later");
        }
    }
}