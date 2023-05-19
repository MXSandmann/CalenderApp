using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebUI.Clients.Contracts;

namespace WebUI.Controllers
{
    [Authorize]
    public class InvitationsController : Controller
    {
        private readonly IInvitationsClient _invitationsClient;

        public InvitationsController(IInvitationsClient invitationsClient)
        {
            _invitationsClient = invitationsClient;
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> InvitationsOverview()
        {
            var invitations = await _invitationsClient.GetAllInvitations();
            return View();
        }

        [HttpGet("[action]")]
        public IActionResult Create()
        {
            return View();
        }
    }
}
