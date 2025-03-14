﻿using Mango.Web.Models;
using Mango.Web.Service.IService;
using Mango.Web.Utility;
using static Mango.Web.Utility.SD;

namespace Mango.Web.Service
{
	public class CouponService:ICouponService
	{
		private readonly IBaseService _baseService;

		public  CouponService(IBaseService baseService)
		{
			_baseService = baseService;
		}

		public async Task<ResponseDto?> CreateCouponsAsync(CouponDto couponDto)
		{
			return await _baseService.sendAsync(new RequestDto()
			{
				ApiType = ApiType.POST,
				Data = couponDto,
				Url = SD.CouponAPIBase + "/api/coupon"
			});
		}

		public async Task<ResponseDto?> DeleteCouponsAsync(int id)
		{
			return await _baseService.sendAsync(new RequestDto()
			{
				ApiType = ApiType.DELETE,
				Url = SD.CouponAPIBase + "/api/coupon/" + id

			});
		}

		public async Task<ResponseDto?> GetAllCouponsAsyc()
		{
			return await _baseService.sendAsync(new RequestDto()
			{
				ApiType = ApiType.GET,
				Url = SD.CouponAPIBase+"/api/coupon"

			});
		}

		public async Task<ResponseDto?> GetCouponAsyc(string couponCode)
		{
			return await _baseService.sendAsync(new RequestDto()
			{
				ApiType = ApiType.GET,
				Url = SD.CouponAPIBase + "/api/coupon/GetByCode/" + couponCode

			});
		}

		public async Task<ResponseDto?> GetCouponByIdAsyc(int id)
		{
			return await _baseService.sendAsync(new RequestDto()
			{
				ApiType = ApiType.GET,
				Url = SD.CouponAPIBase + "/api/coupon/" + id

			});
		}

		public async Task<ResponseDto?> UpdateCouponsAsync(CouponDto couponDto)
		{
			return await _baseService.sendAsync(new RequestDto()
			{
				ApiType = ApiType.PUT,
				Data = couponDto,
				Url = SD.CouponAPIBase + "/api/coupon"
			});
		}
	}
}
