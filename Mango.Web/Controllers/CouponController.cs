using Mango.Web.Models;
using Mango.Web.Service.IService;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Mango.Web.Controllers
{
	public class CouponController : Controller
	{
		private readonly ICouponService _couponService;

		public CouponController(ICouponService couponService)
		{
			_couponService = couponService;
		}

		public async Task<IActionResult> CouponIndex()
		{
			List<CouponDto> list = new();
			ResponseDto? response  = await _couponService.GetAllCouponsAsyc();
			if (response != null && response.isSuccess)
			{
				list = JsonConvert.DeserializeObject<List<CouponDto>>(Convert.ToString(response.Result));
			}
			return View(list);
		}
		[HttpGet]
		public async Task<IActionResult> CouponCreate()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> CouponCreate(CouponDto model)
		{
			if (ModelState.IsValid)
			{
				ResponseDto? response = await _couponService.CreateCouponsAsync(model);
				if (response != null && response.isSuccess)
				{
					return RedirectToAction(nameof(CouponIndex));
				}
				

			}
			return View(model);
        }
		[HttpGet]
		public async Task<IActionResult> CouponDelete(int couponID)
		{
			ResponseDto? response = await _couponService.GetCouponByIdAsyc(couponID);
			if (response != null && response.isSuccess)
			{
				CouponDto model = JsonConvert.DeserializeObject<CouponDto>(Convert.ToString(response.Result));

				return View(model);
			}


				return NotFound();
		}
		[HttpPost]
		public async Task<IActionResult> CouponDelete(CouponDto coupon)
		{
			ResponseDto? response = await _couponService.DeleteCouponsAsync(coupon.CouponId);
			if (response != null && response.isSuccess)
			{
				CouponDto model = JsonConvert.DeserializeObject<CouponDto>(Convert.ToString(response.Result));

				return RedirectToAction(nameof(CouponIndex));
			}


			return View(coupon);
		}
	}
}
