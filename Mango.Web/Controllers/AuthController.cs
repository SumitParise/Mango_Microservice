using Mango.Web.Models;
using Mango.Web.Service.IService;
using Mango.Web.Utility;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Mango.Web.Controllers
{
    public class AuthController : Controller
    {
		private readonly IAuthService _authService;
		private readonly ITokenProvider _tokenProvider;
		public AuthController(IAuthService authService,ITokenProvider tokenProvider)
		{
			_authService = authService;
			_tokenProvider = tokenProvider;
		}

		[HttpGet]
        public IActionResult Login()
        {
            LoginRequestDto loginRequestDto = new();
            return View(loginRequestDto);
        }

		[HttpPost]
		public async Task<IActionResult> Login(LoginRequestDto obj)
		{
			ResponseDto response = await _authService.LoginAsync(obj);

			if(response != null && response.isSuccess)
			{
				LoginResponseDto loginResponse = JsonConvert.DeserializeObject<LoginResponseDto>(Convert.ToString(response.Result));

				await SignInUser(loginResponse);
				_tokenProvider.SetToken(loginResponse.Token);


				TempData["success"] = "Login Successful...!";
				return RedirectToAction("Index","Home");
			}
			else
			{
				ModelState.AddModelError("CustomError", response.Message);
				return View();
			}

			
		}

		[HttpGet]
		public IActionResult Register()
        {
            var listItem = new List<SelectListItem>(){
                new SelectListItem { Text=SD.RoleAdmin,Value=SD.RoleAdmin},
                new SelectListItem { Text=SD.RoleCustomer,Value=SD.RoleCustomer}
            };

            ViewBag.RoleList = listItem;

			return View();
        }

		[HttpPost]
		public async Task<IActionResult> Register(RegistrationRequestDto entity)
		{
			ResponseDto result = await _authService.RegistrationAsync(entity);
			ResponseDto assignRole;

			if(result!=null && result.isSuccess)
			{
				if (string.IsNullOrEmpty(entity.Role))
				{
					entity.Role = SD.RoleCustomer;
				}

				assignRole = await _authService.AssignRoleAsync(entity);

				if(assignRole!=null && assignRole.isSuccess)
				{
					TempData["success"] = "Registration Successful";
					return RedirectToAction(nameof(Login));
				}
			}
			var listItem = new List<SelectListItem>(){
				new SelectListItem { Text=SD.RoleAdmin,Value=SD.RoleAdmin},
				new SelectListItem { Text=SD.RoleCustomer,Value=SD.RoleCustomer}
			};

			ViewBag.RoleList = listItem;

			return View(entity);

		}

		[HttpGet]
		public async Task<IActionResult> Logout()
		{
			await HttpContext.SignOutAsync();
			_tokenProvider.ClearToken();
			return RedirectToAction("Index","Home");
		}

		public async Task SignInUser(LoginResponseDto model)
		{
			var handler = new JwtSecurityTokenHandler(); 
			var jwt = handler.ReadJwtToken(model.Token);  // get token

			var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);

			//Extracting all claims

			identity.AddClaim(new Claim(JwtRegisteredClaimNames.Email, 
				jwt.Claims.FirstOrDefault(u => u.Type == JwtRegisteredClaimNames.Email).Value));

			identity.AddClaim(new Claim(JwtRegisteredClaimNames.Sub,
				jwt.Claims.FirstOrDefault(u => u.Type == JwtRegisteredClaimNames.Sub).Value));

			identity.AddClaim(new Claim(JwtRegisteredClaimNames.Name,
				jwt.Claims.FirstOrDefault(u => u.Type == JwtRegisteredClaimNames.Name).Value));

			identity.AddClaim(new Claim(ClaimTypes.Name,
			jwt.Claims.FirstOrDefault(u => u.Type == JwtRegisteredClaimNames.Email).Value));

			//set principal

			var principal = new ClaimsPrincipal(identity);

			

			await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
		}
	}
}
