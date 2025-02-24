using Mango.Services.AuthAPI.Model.Dto;

namespace Mango.Services.AuthAPI.Services.IService
{
	public interface IAuthService
	{
		Task<string> Register(RegistrationRequestDto registrationRequestDto);

		Task<LoginResponseDto> Login(LoginRequestDto loginRequestDto);
	}
}
