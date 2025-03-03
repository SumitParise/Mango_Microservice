﻿using Mango.Web.Models;
using Mango.Web.Utility;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

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
            var listItem = new List<SelectListItem>(){
                new SelectListItem { Text=SD.RoleAdmin,Value=SD.RoleAdmin},
                new SelectListItem { Text=SD.RoleCustomer,Value=SD.RoleCustomer}
            };

            ViewBag.RoleList = listItem;

			return View();
        }

		[HttpGet]
		public IActionResult Logout()
		{
			return View();
		}
	}
}
