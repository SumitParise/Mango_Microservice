namespace Mango.Web.Models;

public class ResponseDto
{
	public object Result { get; set; }

	public bool isSuccess { get; set; }

	public string Message { get; set; }
}
