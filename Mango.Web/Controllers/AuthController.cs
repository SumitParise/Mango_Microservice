using Mango.Web.Models;
using Mango.Web.Service.IService;
using Mango.Web.Utility;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
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
		public IActionResult Logout()
		{
			return View();
		}
	}
}
