using ApplicationCore.Services.Contracts;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebUI.Models.Dtos;

namespace WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AccountController : ControllerBase
{
    private readonly ILogger<AccountController> _logger;
    private readonly IUserService _userService;

    public AccountController(ILogger<AccountController> logger, IUserService userService)
    {
        _logger = logger;
        _userService = userService;
    }

    [HttpGet("[action]")]
    public async Task<IActionResult> Login([FromQuery] string username, string password)
    {
        _logger.LogInformation("--> Received username: {value1}, password: {value2}", username, password);

        var token = await _userService.LoginUser(username, password);
        if (string.IsNullOrWhiteSpace(token))
            return Unauthorized("Wrong username or password");
        return Ok(token);
    }

    [HttpPost("[action]")]
    public async Task<IActionResult> Register([FromBody] UserRegistrationDto userRegistrationDto)
    {
        _logger.LogInformation("--> Received registration dto", JsonConvert.SerializeObject(userRegistrationDto));

        var newUser = await _userService.CreateUser(userRegistrationDto.UserName, userRegistrationDto.Password, userRegistrationDto.Email, userRegistrationDto.Role);
        if (newUser == null
            || newUser.Id == Guid.Empty)
            return BadRequest("Unable to create a new user");
        return Ok();
    }
}
