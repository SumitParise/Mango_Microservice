using Microsoft.AspNetCore.Authentication;

namespace Mango.Services.CouponAPI.Models
{
	public class Coupon
	{
		public int CouponId { get; set; }

		public string CouponCode { get; set; }

		public double discountAmount { get; set; }
		public int MinAmount { get; set; }


	}
}
