﻿using AutoMapper;
using HotelListing.DataAccess.IRepository;
using HotelListing.Models;
using HotelListing.Models.DTOs.Hotel;
using HotelListing.Utility;
using Microsoft.AspNetCore.Authorization;
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
            List<HotelDto> hotelDtoList = _mapper.Map<List<HotelDto>>(hotels);
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
    [HttpGet("{id:int}", Name = "GetHotel")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetHotel(int id)
    {
        try
        {
            var hotel = await _unitOfWork.Hotels.GetAsync(c => c.Id == id, new List<string> { "Country" });
            HotelDto hotelDto = _mapper.Map<HotelDto>(hotel);

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


    // POST: api/Hotels
    [HttpPost]
    [Authorize(Roles = AppRoles.Admin)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> CreateHotel(CreateHotelDto model)
    {
        try
        {
            Hotel hotel = _mapper.Map<Hotel>(model);
            
            Country? country = await _unitOfWork.Countries.GetAsync(c => c.Id == model.CountryId);

            if (country is null)
                return BadRequest(new
                {
                    errors = new List<object>() { new { country = "No country found matches your selection." } }
                });

            await _unitOfWork.Hotels.Add(hotel);

            await _unitOfWork.SaveAsync();

            // CreatedAtRoute => an url for hotel will be in Headers => Location
            return CreatedAtRoute(nameof(GetHotel), new {id = hotel.Id}, hotel);
        }
        catch (Exception e)
        {
            _logger.LogError(e, $"Error in [{nameof(HotelController)}] controller" +
                                $" and [{nameof(CreateHotel)}] Action");
            return StatusCode(500, "Internal server error. Try again later");
        }
    }
}