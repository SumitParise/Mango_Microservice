﻿using Mango.Services.AuthAPI.DAL;
using Mango.Services.AuthAPI.Model.Dto;
using Mango.Services.AuthAPI.Models.Dto;
using Mango.Services.AuthAPI.Services.IService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Mango.Services.AuthAPI.Controllers
{
	[Route("api/auth")]
	[ApiController]
	public class AuthApiController : ControllerBase
	{

		private readonly IAuthService _authService;
		private readonly ResponseDto _responseDto;

		public AuthApiController(IAuthService authService)
		{
			_authService = authService;
			_responseDto = new();
		}

		[HttpPost("register")]
		public async Task<IActionResult> Register([FromBody]RegistrationRequestDto registrationRequestDto)
		{
			var errorMessage = await _authService.Register(registrationRequestDto);

			if (!string.IsNullOrEmpty(errorMessage))
			{
				_responseDto.isSuccess = false;
				_responseDto.Message = errorMessage;
				return BadRequest(_responseDto);
			}

			return Ok(_responseDto);
		}

		[HttpPost("login")]
		public async Task<IActionResult> Login()
		{
			return Ok();
		}
	}
}
