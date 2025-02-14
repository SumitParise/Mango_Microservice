using Microsoft.AspNetCore.Authentication;
using System.ComponentModel.DataAnnotations;

namespace Mango.Services.CouponAPI.Models
{
	public class Coupon
	{
		public int CouponId { get; set; }

		[Required]
		public string CouponCode { get; set; }
		[Required]
		public double discountAmount { get; set; }
		[Required]
		public int MinAmount { get; set; }


	}
}
