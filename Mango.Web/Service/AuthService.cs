﻿using Mango.Web.Models;
using Mango.Web.Service.IService;
using Mango.Web.Utility;
using static Mango.Web.Utility.SD;

namespace Mango.Web.Service
{
	public class AuthService : IAuthService
	{

		private readonly IBaseService _baseService;

		public AuthService(IBaseService baseService)
		{
			_baseService = baseService;
		}

		public async Task<ResponseDto?> AssignRoleAsync(RegistrationRequestDto registrationRequestDto)
		{
			return await _baseService.sendAsync(new RequestDto()
			{
				ApiType = ApiType.POST,
				Data = registrationRequestDto,
				Url = SD.AuthAPIBase + "/api/auth"+ "/AssignRole"
			});
		}

		public async Task<ResponseDto?> LoginAsync(LoginRequestDto loginRequestDto)
		{
			return await _baseService.sendAsync(new RequestDto()
			{
				ApiType = ApiType.POST,
				Data = loginRequestDto,
				Url = SD.AuthAPIBase + "/api/auth"+ "/login"
			});
		}

		public async Task<ResponseDto?> RegistrationAsync(RegistrationRequestDto registrationRequestDto)
		{
			return await _baseService.sendAsync(new RequestDto()
			{
				ApiType = ApiType.POST,
				Data = registrationRequestDto,
				Url = SD.AuthAPIBase + "/api/auth"+ "/register"
			});
		}
	}
}
