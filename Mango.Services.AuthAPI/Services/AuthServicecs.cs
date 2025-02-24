﻿using Mango.Services.AuthAPI.DAL;
using Mango.Services.AuthAPI.Model;
using Mango.Services.AuthAPI.Model.Dto;
using Mango.Services.AuthAPI.Services.IService;
using Microsoft.AspNetCore.Identity;

namespace Mango.Services.AuthAPI.Services
{
	public class AuthServicecs : IAuthService
	{
		private readonly AppDbContext _appDbContext;
		private readonly UserManager<ApplicationUsers> userManager;
		private readonly RoleManager<IdentityRole> roleManager;

		public AuthServicecs(AppDbContext appDbContext, UserManager<ApplicationUsers> userManager, RoleManager<IdentityRole> roleManager)
		{
			_appDbContext = appDbContext;
			this.userManager = userManager;
			this.roleManager = roleManager;
		}

		public async Task<LoginResponseDto> Login(LoginRequestDto loginRequestDto)
		{
			LoginRequestDto loginRequest = new()
			{
				UserName = loginRequestDto.UserName,
				Password = loginRequestDto.Password
			};

			return await Login(loginRequest);
		}

		public async Task<string> Register(RegistrationRequestDto registrationRequestDto)
		{
			ApplicationUsers user = new()
			{
				UserName = registrationRequestDto.Email,
				Email = registrationRequestDto.Email,
				NormalizedEmail = registrationRequestDto.Email.ToUpper(),
				name = registrationRequestDto.Name,
				PhoneNumber = registrationRequestDto.PhoneNumber
			};
			try
			{
				var result = await userManager.CreateAsync(user, registrationRequestDto.Password);

				if (result.Succeeded)
				{
					var userToReturn = _appDbContext.AppllicationUsers.First(u => u.UserName == registrationRequestDto.Email);

					UserDto userDto = new()
					{
						ID = userToReturn.Id,
						Name = userToReturn.name,
						Email = userToReturn.Email,
						PhoneNumber = userToReturn.PhoneNumber
					};

					return "";
				}
				else
				{
					return result.Errors.FirstOrDefault().Description;
				}
			}
			catch (Exception ex)
			{

			}
			return "Error Encountred";
		}
	}
}
