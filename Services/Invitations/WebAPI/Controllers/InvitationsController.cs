using ApplicationCore.Models.Dto;
using ApplicationCore.Models.Entities;
using ApplicationCore.Services.Contracts;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class InvitationsController : ControllerBase
{    
    private readonly IInvitationService _invitationService;
    private readonly IMapper _mapper;
    private readonly ILogger<InvitationsController> _logger;

    public InvitationsController(IInvitationService invitationService, IMapper mapper, ILogger<InvitationsController> logger)
    {
        _invitationService = invitationService;
        _mapper = mapper;
        _logger = logger;
    }

    [HttpPost("[action]")]
    public async Task<IActionResult> Create([FromBody] InvitationDto dto)
    {
        _logger.LogInformation("--> Received a Invitation dto to create: {value}", JsonConvert.SerializeObject(dto));
        var newInvitation = await _invitationService.AddInvitation(_mapper.Map<Invitation>(dto), dto.UserName);
        return Ok(_mapper.Map<InvitationDto>(newInvitation));
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var invitations = await _invitationService.GetAllInvitations();
        var dtos = _mapper.Map<IEnumerable<InvitationDto>>(invitations);
        return Ok(dtos);
    }
}