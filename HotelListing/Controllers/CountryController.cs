using AutoMapper;
using HotelListing.DataAccess.IRepository;
using HotelListing.Models.DTOs.Country;
using HotelListing.Utility;
using Microsoft.AspNetCore.Authorization;
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
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetCountries()
    {
        try
        {
            var countries = await _unitOfWork.Countries.GetAllAsync();
            List<CountryDto> countryDtoList = _mapper.Map<List<CountryDto>>(countries);
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
    [Authorize(Roles = Roles.Admin)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetCountry(int id)
    {
        try
        {
            var country = await _unitOfWork.Countries.GetAsync(c => c.Id == id, new List<string>{"Hotels"});
            CountryDto countryDto= _mapper.Map<CountryDto>(country);

            if (countryDto is null)
            {
                return NotFound();
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