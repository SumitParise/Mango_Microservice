﻿using Mango.Web.Models;

namespace Mango.Web.Service.IService
{
	public interface IBaseService
	{
		Task<ResponseDto?> sendAsync(RequestDto requestDto);
	}
}
