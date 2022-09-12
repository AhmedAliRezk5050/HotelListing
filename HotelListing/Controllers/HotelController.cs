using AutoMapper;
using HotelListing.DataAccess.IRepository;
using HotelListing.Models.DTOs.Hotel;
using Microsoft.AspNetCore.Mvc;

namespace HotelListing.Controllers;

[Route("api/[controller]")]
[ApiController]
public class HotelController : ControllerBase
{
    private readonly ILogger<HotelController> _logger;

    private readonly IUnitOfWork _unitOfWork;

    private readonly IMapper _mapper;

    public HotelController(ILogger<HotelController> logger, IUnitOfWork unitOfWork, IMapper mapper)
    {
        _logger = logger;

        _unitOfWork = unitOfWork;

        _mapper = mapper;
    }

    // GET: api/Hotels
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetHotels()
    {
        try
        {
            var hotels = await _unitOfWork.Hotels.GetAllAsync();
            List<HotelDTO> hotelDtoList = _mapper.Map<List<HotelDTO>>(hotels);
            return Ok(hotelDtoList);
        }
        catch (Exception e)
        {
            _logger.LogError(e, $"Error in {nameof(HotelController)} controller" +
                                $" and {nameof(GetHotels)} Action");
            return StatusCode(500, "Internal server error. Try again later");
        }
    }

    // GET: api/Hotels/5
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetHotel(int id)
    {
        try
        {
            var hotel = await _unitOfWork.Hotels.GetAsync(c => c.Id == id, new List<string> {"Country"});
            HotelDTO hotelDto = _mapper.Map<HotelDTO>(hotel);

            if (hotelDto is null)
            {
                return NotFound();
            }
            return Ok(hotel);
        }
        catch (Exception e)
        {
            _logger.LogError(e, $"Error in [{nameof(HotelController)}] controller" +
                                $" and [{nameof(GetHotels)}] Action");
            return StatusCode(500, "Internal server error. Try again later");
        }
    }
}