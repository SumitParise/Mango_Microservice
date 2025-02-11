using Mango.Web.Models;
using Mango.Web.Service.IService;
using Newtonsoft.Json;
using System.Net;
using System.Text;
using static Mango.Web.Utility.SD;

namespace Mango.Web.Service
{
	public class BaseService : IBaseService
	{
		private readonly IHttpClientFactory _httpClientFactory;

		public BaseService(IHttpClientFactory httpClientFactory)
		{
			_httpClientFactory = httpClientFactory;
		}

		public async Task<ResponseDto?> sendAsync(RequestDto requestDto)
		{
			try
			{
				HttpClient client = _httpClientFactory.CreateClient("MangoAPI");
				//create obj of request
				HttpRequestMessage message = new();
				message.Headers.Add("Accept", "application/json");
				//token

				message.RequestUri = new Uri(requestDto.Url);

				if (requestDto.Data != null)
				{
					message.Content = new StringContent(JsonConvert.SerializeObject(requestDto.Data), Encoding.UTF8, "application/json");
				}

				HttpResponseMessage? apiResponse = null;

				switch (requestDto.ApiType)
				{
					case ApiType.POST:
						message.Method = HttpMethod.Post;
						break;
					case ApiType.DELETE:
						message.Method = HttpMethod.Delete;
						break;
					case ApiType.PUT:
						message.Method = HttpMethod.Put;
						break;
					default:
						message.Method = HttpMethod.Get;
						break;
				}

				apiResponse = await client.SendAsync(message);  // we send req and get response and will stored it in apiResponse var

				switch (apiResponse.StatusCode)
				{

					case HttpStatusCode.NotFound:
						return new() { isSuccess = false, Message = "Not Found" };
					case HttpStatusCode.Forbidden:
						return new() { isSuccess = false, Message = "Access Denied" };
					case HttpStatusCode.Unauthorized:
						return new() { isSuccess = false, Message = "Unauthorized" };
					case HttpStatusCode.InternalServerError:
						return new() { isSuccess = false, Message = "Internal Server Error" };
					default: // by default for getting Success
						var apiContent = await apiResponse.Content.ReadAsStringAsync();
						var apiResponseDto = JsonConvert.DeserializeObject<ResponseDto>(apiContent);
						return apiResponseDto;

				}
			}  // if any exception happen we caught here and return res dto
			catch (Exception ex)
			{
				var dto = new ResponseDto()
				{
					Message = ex.Message.ToString(),
					isSuccess = false,
				};
				return dto;
			}
		}
	}
}
