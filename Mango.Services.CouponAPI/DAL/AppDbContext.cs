using Mango.Services.CouponAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace Mango.Services.CouponAPI.DAL
{
	public class AppDbContext : DbContext
	{
		public AppDbContext(DbContextOptions options) : base(options)
		{

		}

		public DbSet<Coupon> coupons { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);


			modelBuilder.Entity<Coupon>().HasData(new Coupon
			{
				CouponId = 1,
				CouponCode = "10OFF",
				discountAmount = 10,
				MinAmount = 20,
			});

			modelBuilder.Entity<Coupon>().HasData(new Coupon
			{
				CouponId = 2,
				CouponCode = "20OFF",
				discountAmount = 20,
				MinAmount = 40,
			});
		}
	}
}
