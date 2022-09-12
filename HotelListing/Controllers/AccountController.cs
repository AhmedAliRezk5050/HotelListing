using AutoMapper;
using HotelListing.Models;
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
    
    
}