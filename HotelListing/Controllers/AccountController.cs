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
            var user = _mapper.Map<AppUser>(model);

            var result = await _userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(error.Code, error.Description);
                }
    
                return BadRequest(new
                {
                    // unify response shape
                    errors = result.Errors.Zip(result.Errors,
                            (x, y) =>
                                new { Name = x.Code, Header = new List<string>() { y.Description } })
                        .ToDictionary(kvp => kvp.Name, kvp => kvp.Header)
                });
            }


            try
            {
                var addRolesResult = await _userManager.AddToRolesAsync(user, model.Roles);

                if (!addRolesResult.Succeeded) throw new Exception("Error adding roles");
            }
            catch (Exception e)
            {
                await DeleteUserIfExist(model.UserName);
                return BadRequest();
            }
            
            return StatusCode(StatusCodes.Status201Created);
        }
        catch (Exception e)
        {
            _logger.LogError(e, $"Error in {nameof(AccountController)} controller" +
                                $" and {nameof(Register)} Action");

            await DeleteUserIfExist(model.UserName);

            return StatusCode(500, "Internal server error");
        }
    }

    private async Task DeleteUserIfExist(string userName)
    {
        var userFromDb = await _userManager.FindByNameAsync(userName);
        if (userFromDb is not null)
        {
            await _userManager.DeleteAsync(userFromDb);
        }
    }
}
