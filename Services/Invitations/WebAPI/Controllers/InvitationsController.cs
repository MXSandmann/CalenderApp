using ApplicationCore.Models.Dto;
using ApplicationCore.Models.Entities;
using ApplicationCore.Services.Contracts;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class InvitationsController : ControllerBase
{    
    private readonly IInvitationService _invitationService;
    private readonly IMapper _mapper;

    public InvitationsController(IInvitationService invitationService, IMapper mapper)
    {        
        _invitationService = invitationService;
        _mapper = mapper;
    }

    [HttpPost("[action]")]
    public async Task<IActionResult> Create([FromBody] InvitationDto dto)
    {
        var newInvitation = await _invitationService.AddInvitation(_mapper.Map<Invitation>(dto));
        return Ok(_mapper.Map<InvitationDto>(newInvitation));
    }
}