using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebUI.Clients.Contracts;
using WebUI.Models.ViewModels;
using WebUI.Models.Dtos;

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
                return BadRequest(ModelState);            
            
            var claims = await _client.LoginUser(_mapper.Map<UserDto>(loginViewModel));

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claims);

            return RedirectToAction("Index", "Home");
        }
    }
}
