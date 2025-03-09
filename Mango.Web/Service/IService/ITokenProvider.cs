namespace Mango.Web.Service.IService
{
	public interface ITokenProvider
	{
		void SetToken(string Token);
		string? GetToken();
		void ClearToken();
	}
}
