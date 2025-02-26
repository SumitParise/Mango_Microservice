using Mango.Services.AuthAPI.Model;

namespace Mango.Services.AuthAPI.Services.IService
{
	public interface IJwtTokenGenerator
	{
		string GenerateToken(ApplicationUsers applicationUsers);
	}
}
