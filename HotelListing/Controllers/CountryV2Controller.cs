using AutoMapper;
using HotelListing.DataAccess;
using HotelListing.DataAccess.IRepository;
using HotelListing.Models;
using HotelListing.Models.DataTypes;
using HotelListing.Models.DTOs.Country;
using HotelListing.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using X.PagedList;

namespace HotelListing.Controllers;

[ApiVersion("2.0")]
// api/2/country
[Route("api/{v:apiVersion}/country")]
[ApiController]
public class CountryV2Controller : ControllerBase
{
    private readonly ILogger<CountryV2Controller> _logger;

    private readonly DataContext _context;

    private readonly IMapper _mapper;

    public CountryV2Controller(ILogger<CountryV2Controller> logger, DataContext context, IMapper mapper)
    {
        _logger = logger;

        _context = context;

        _mapper = mapper;
    }

    // GET: api/Countries
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public IActionResult GetCountries()
    {
        return Ok(_context.Countries);
    }
}