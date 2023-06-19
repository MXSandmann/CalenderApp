using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebUI.Clients.Contracts;
using WebUI.Extensions;
using WebUI.Models.Dtos;
using WebUI.Models.Enums;
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
        private readonly IValidator<CreateInvitationViewModel> _validator;

        public InvitationsController(IInvitationsClient invitationsClient, IMapper mapper, IHttpContextAccessor httpContextAccessor, IValidator<CreateInvitationViewModel> validator)
        {
            _invitationsClient = invitationsClient;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            _validator = validator;
        }

        /// <summary>
        /// Create an invitations for user to a particular evets
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]/{eventId:guid}")]
        public IActionResult CreateToEvent()
        {
            return View();
        }

        /// <summary>
        /// Create an invitations for user to a particular evets
        /// </summary>
        /// <returns></returns>
        [HttpPost("[action]/{eventId:guid}")]
        public async Task<IActionResult> CreateToEvent(CreateInvitationViewModel createInvitationViewModel, [FromRoute] Guid eventId)
        {
            var validationResult = _validator.Validate(createInvitationViewModel);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            var user = _httpContextAccessor.HttpContext!.User.GetUserNameFromClaims();
            createInvitationViewModel.EventId = eventId;
            createInvitationViewModel.Role = RoleToInvite.User;
            var dto = _mapper.Map<InvitationDto>(createInvitationViewModel);
            dto.UserName = user;
            await _invitationsClient.CreateInvitation(dto);
            return RedirectToAction("Index", "Home");
        }

        [HttpGet("[action]")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Create(CreateInvitationViewModel createInvitationViewModel)
        {
            var validationResult = _validator.Validate(createInvitationViewModel);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            var user = _httpContextAccessor.HttpContext!.User.GetUserNameFromClaims();
            var dto = _mapper.Map<InvitationDto>(createInvitationViewModel);
            dto.UserName = user;
            await _invitationsClient.CreateInvitation(dto);
            return RedirectToAction("Index", "Home");
        }
    }
}
