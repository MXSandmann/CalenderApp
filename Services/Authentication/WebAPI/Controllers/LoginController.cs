using ApplicationCore.Services.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LoginController : ControllerBase
{
    private readonly ILogger<LoginController> _logger;
    private readonly IUserService _userService;

    public LoginController(ILogger<LoginController> logger, IUserService userService)
    {
        _logger = logger;
        _userService = userService;
    }

    [HttpGet]
    public async Task<IActionResult> Login([FromQuery] string username, string password)
    {
        _logger.LogInformation("--> Received username: {value1}, password: {value2}", username, password);

        var token = await _userService.LoginUser(username, password);
        if (string.IsNullOrWhiteSpace(token))
            return Unauthorized("Wrong username or password");
        return Ok(token);
    }
}
