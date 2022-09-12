using AutoMapper;
using HotelListing.Models;
using HotelListing.Models.Dtos.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HotelListing.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AccountController : ControllerBase
{
    private readonly UserManager<AppUser> _userManager;
    private readonly SignInManager<AppUser> _signInManager;
    private readonly IMapper _mapper;
    private readonly ILogger<AccountController> _logger;

    public AccountController(UserManager<AppUser>
            userManager, SignInManager<AppUser> signInManager,
        IMapper mapper, ILogger<AccountController> logger)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _mapper = mapper;
        _logger = logger;
    }

    [HttpPost("register")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Register(CreateUserDto model)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Failed to register");
            }

            var user = _mapper.Map<AppUser>(model);

            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded) return StatusCode(StatusCodes.Status201Created);
            
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(error.Code, error.Description);
            }

            return BadRequest(ModelState);

        }
        catch (Exception e)
        {
            _logger.LogError(e, $"Error in {nameof(AccountController)} controller" +
                                $" and {nameof(Register)} Action");
            return StatusCode(500, "Internal server error");
        }
    }
}