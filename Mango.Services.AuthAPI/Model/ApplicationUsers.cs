using Microsoft.AspNetCore.Identity;

namespace Mango.Services.AuthAPI.Model
{
	public class ApplicationUsers:IdentityUser
	{
		public string name { get; set; }
	}
}
