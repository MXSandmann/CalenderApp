using ApplicationCore.Models.Enums;
using ApplicationCore.Providers.Contracts;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LoginController : ControllerBase
{    
    private readonly ILogger<LoginController> _logger;
    private readonly IJwtProvider _jwtProvider;

    public LoginController(ILogger<LoginController> logger, IJwtProvider jwtProvider)
    {
        _logger = logger;
        _jwtProvider = jwtProvider;
    }

    [HttpGet]
    public async Task<IActionResult> Login([FromQuery] string username, string password)
    {
        _logger.LogInformation("--> Received username: {value1}, password: {value2}", username, password);
        //return Ok("eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCIsImtpZCI6IjEyMyJ9.eyJzdWIiOiIxMjM0NTY3ODkwIiwibmFtZSI6IkpvaG4gRG9lIiwiaWF0IjoxNTE2MjM5MDIyfQ.W9KeGmPoeI55Kowu5li8Fu_1wWpI4SUAkWj2eeWxLJs");

        var claims = new List<Claim>
        {
            new Claim("Id", "1234567890"),
            new Claim("UserName", "John Doe"),
            new Claim(ClaimTypes.Role, Role.User.ToString())
        };

        var token = _jwtProvider.CreateToken(claims);
        return Ok(token);
    }

    //private static string CreateToken()
    //{
    //    var key = "e3b0c44298fc1c149afbf4c8996fb92427ae41e4649b934ca495991b7852b855";
    //    var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key))
    //    {
    //        KeyId = "123"
    //    };

    //    var claims = new List<Claim>
    //    {
    //        new Claim("sub", "1234567890"),
    //        new Claim("name", "John Doe")
    //    };


    //    var handler = new JwtSecurityTokenHandler();
    //    var signingCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256Signature);
    //    var identity = new ClaimsIdentity(claims);
    //    var token = handler.CreateJwtSecurityToken(subject: identity,
    //        signingCredentials: signingCredentials);
    //    return handler.WriteToken(token);
    //}
}
