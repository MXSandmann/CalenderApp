using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebUI.Clients.Contracts;
using WebUI.Models;
using WebUI.Models.Dtos;
using WebUI.Models.ViewModels;

namespace WebUI.Controllers
{
    [Route("[controller]")]
    public class AccountController : Controller
    {
        private readonly IAuthenticationClient _client;
        private readonly ILogger<AccountController> _logger;
        private readonly IMapper _mapper;
        private readonly IValidator<RegisterViewModel> _registerValidator;

        public AccountController(IAuthenticationClient client, IMapper mapper, ILogger<AccountController> logger, IValidator<RegisterViewModel> registerValidator)
        {
            _client = client;
            _mapper = mapper;
            _logger = logger;
            _registerValidator = registerValidator;
        }

        [HttpGet("[action]")]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel, [FromQuery] string? returnUrl)
        {
            _logger.LogInformation("--> login action started: {value}", JsonConvert.SerializeObject(loginViewModel));
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var claims = await _client.LoginUser(_mapper.Map<UserDto>(loginViewModel));

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claims);

            if (!string.IsNullOrEmpty(returnUrl))
                return Redirect(returnUrl);

            return RedirectToAction("Index", "Home");
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }

        [HttpGet("[action]")]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Register(RegisterViewModel registerViewModel)
        {
            _logger.LogInformation("--> Registrating new user: {value}", JsonConvert.SerializeObject(registerViewModel));
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var validationResult = _registerValidator.Validate(registerViewModel);
            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            await _client.RegisterNewUser(_mapper.Map<UserRegistrationDto>(registerViewModel));

            return RedirectToAction("Index", "Home");
        }
    }
}
