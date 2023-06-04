using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebUI.Clients.Contracts;
using WebUI.Models.Dtos;
using WebUI.Models.ViewModels;

namespace WebUI.Controllers
{
    [Route("[controller]")]
    [Authorize]
    public class InvitationsController : Controller
    {
        private readonly IInvitationsClient _invitationsClient;
        private readonly IMapper _mapper;

        public InvitationsController(IInvitationsClient invitationsClient, IMapper mapper)
        {
            _invitationsClient = invitationsClient;
            _mapper = mapper;
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> InvitationsOverview()
        {
            var invitations = await _invitationsClient.GetAllInvitations();
            return View(invitations);
        }

        [HttpGet("[action]/{eventId:guid}")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost("[action]/{eventId:guid}")]
        public async Task<IActionResult> Create(CreateInvitationViewModel createInvitationViewModel, [FromRoute] Guid eventId)
        {
            createInvitationViewModel.EventId = eventId;
            var dto = _mapper.Map<InvitationDto>(createInvitationViewModel);
            await _invitationsClient.CreateInvitation(dto);
            return RedirectToAction("Index", "Home");
        }
    }
}
