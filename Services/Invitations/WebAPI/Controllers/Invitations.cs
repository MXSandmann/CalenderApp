using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class Invitations : ControllerBase
{
    private readonly ILogger<Invitations> _logger;

    public Invitations(ILogger<Invitations> logger)
    {
        _logger = logger;
    }    
}
