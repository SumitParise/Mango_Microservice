using Mango.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace Mango.Web.Controllers
{
    public class AuthController : Controller
    {
        [HttpGet]
        public IActionResult Login()
        {
            LoginRequestDto loginRequestDto = new();
            return View(loginRequestDto);
        }

		[HttpGet]
		public IActionResult Register()
        {
            return View();
        }

		[HttpGet]
		public IActionResult Logout()
		{
			return View();
		}
	}
}
