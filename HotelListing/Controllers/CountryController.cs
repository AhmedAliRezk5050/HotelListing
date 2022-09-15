﻿using AutoMapper;
using HotelListing.DataAccess.IRepository;
using HotelListing.Models;
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
    [HttpGet("{id:int}", Name = "GetCountry")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetCountry(int id)
    {
        try
        {
            var country = await _unitOfWork.Countries.GetAsync(c => c.Id == id, new List<string> { "Hotels" });
            CountryDto countryDto = _mapper.Map<CountryDto>(country);

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

    // POST: api/Countries
    [HttpPost]
    [Authorize(Roles = AppRoles.Admin)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> CreateCountry(CreateCountryDto model)
    {
        try
        {
            Country country = _mapper.Map<Country>(model);

            await _unitOfWork.Countries.Add(country);

            await _unitOfWork.SaveAsync();

            return CreatedAtRoute(nameof(GetCountry), new { id = country.Id }, country);
        }
        catch (Exception e)
        {
            _logger.LogError(e, $"Error in [{nameof(CountryController)}] controller" +
                                $" and [{nameof(CreateCountry)}] Action");
            return StatusCode(500, "Internal server error. Try again later");
        }
    }

    // PUT: api/Countries/id
    [HttpPut("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> UpdateCountry(int id, UpdateCountryDto dto)
    {
        try
        {
            if (id < 1 || id != dto.Id) return BadRequest();

            Country? country = await _unitOfWork.Countries.GetAsync(c => c.Id == id);

            if (country is null) return BadRequest();
            
            _unitOfWork.Countries.Update(_mapper.Map(dto, country));

            await _unitOfWork.SaveAsync();

            return NoContent();
        }
        catch (Exception e)
        {
            _logger.LogError(e, $"Error in [{nameof(CountryController)}] controller" +
                                $" and [{nameof(UpdateCountry)}] Action");
            return StatusCode(500, "Internal server error. Try again later");
        }
    }
    
    // DELETE: api/countries/id
    [HttpDelete("{id:int}")]
    [Authorize(Roles = AppRoles.Admin)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> DeleteCountry(int id)
    {
        try
        {
            if (id < 1) return BadRequest();

            Country? country = await _unitOfWork.Countries.GetAsync(c => c.Id == id);

            if (country is null) return NotFound();

            _unitOfWork.Countries.Remove(country);

            await _unitOfWork.SaveAsync();

            return NoContent();
        }
        catch (Exception e)
        {
            _logger.LogError(e, $"Error in [{nameof(CountryController)}] controller" +
                                $" and [{nameof(DeleteCountry)}] Action");
            return StatusCode(500, "Internal server error. Try again later");
        }
    }
}