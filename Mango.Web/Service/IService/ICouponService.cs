using Mango.Web.Models;

namespace Mango.Web.Service.IService
{
	public interface ICouponService
	{
		Task<ResponseDto?> GetCouponAsyc(string couponCode);
		Task<ResponseDto?> GetAllCouponsAsyc();
		Task<ResponseDto?> GetCouponByIdAsyc(int id);

		Task<ResponseDto?> CreateCouponsAsync(CouponDto couponDto);
		Task<ResponseDto?> UpdateCouponsAsync(CouponDto couponDto);

		Task<ResponseDto?> DeleteCouponsAsync(int id);


	}
}
