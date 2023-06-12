using CardProductAPI.Models;
using CardProductAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace CardProductAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly IConfiguration _configuration;
    private readonly ITokenService _tokenService;

    public UsersController(IConfiguration configuration, ITokenService tokenService)
    {
        this._configuration = configuration;
        this._tokenService = tokenService;
    }

    [HttpPost]
    [Route("sign-in")]
    public IActionResult Post(User userModel)
    {
        return Ok(_tokenService.GenerateToken(_configuration["Jwt:Auth:Key"], _configuration["Jwt:Auth:ValidIssuer"], userModel));
    }
}