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
        private readonly IHttpContextAccessor _httpContextAccessor;

        public InvitationsController(IInvitationsClient invitationsClient, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _invitationsClient = invitationsClient;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }        

        [HttpGet("[action]/{eventId:guid}")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost("[action]/{eventId:guid}")]
        public async Task<IActionResult> Create(CreateInvitationViewModel createInvitationViewModel, [FromRoute] Guid eventId)
        {
            var user = _httpContextAccessor.HttpContext?.User.Identities.First().Claims.FirstOrDefault(x => x.Type.Equals("UserName"))?.Value ?? "User";
            createInvitationViewModel.EventId = eventId;
            var dto = _mapper.Map<InvitationDto>(createInvitationViewModel);
            dto.UserName = user;
            await _invitationsClient.CreateInvitation(dto);
            return RedirectToAction("Index", "Home");
        }
    }
}
