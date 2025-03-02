using Mango.Web.Models;

namespace Mango.Web.Service.IService
{
	public interface IAuthService
	{
		Task<ResponseDto> LoginAsync(LoginRequestDto loginRequestDto);
		Task<ResponseDto> RegistrationAsync(LoginRequestDto loginRequestDto);
		Task<ResponseDto> AssignRoleAsync(LoginRequestDto loginRequestDto);
	}
}
