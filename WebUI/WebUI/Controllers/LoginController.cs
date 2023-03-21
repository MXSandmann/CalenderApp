using ApplicationCore.Models.Dtos;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebUI.Clients.Contracts;
using WebUI.Models.ViewModels;

namespace WebUI.Controllers
{
    [Route("[controller]")]
    public class LoginController : Controller
    {
        private readonly IAuthenticationClient _client;
        private readonly ILogger<LoginController> _logger;
        private readonly IMapper _mapper;

        public LoginController(IAuthenticationClient client, IMapper mapper, ILogger<LoginController> logger)
        {
            _client = client;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
            _logger.LogInformation("--> login action started: {value}", JsonConvert.SerializeObject(loginViewModel));
            if(!ModelState.IsValid)
            {
                _logger.LogInformation("--> view model invalid");
                return BadRequest(ModelState);
            }
            _logger.LogInformation("--> getting token");
            var token = await _client.LoginUser(_mapper.Map<UserDto>(loginViewModel));            
            return RedirectToAction("Index", "Home");
        }
    }
}
