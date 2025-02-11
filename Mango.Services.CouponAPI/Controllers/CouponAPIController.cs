using AutoMapper;
using Mango.Services.CouponAPI.DAL;
using Mango.Services.CouponAPI.Models;
using Mango.Services.CouponAPI.Models.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Mango.Services.CouponAPI.Controllers
{
	[Route("api/coupon")]
	[ApiController]
	public class CouponAPIController : ControllerBase
	{
		private readonly AppDbContext _db;
		private ResponseDto _response;
		private IMapper _mapper;
		public CouponAPIController(AppDbContext db, IMapper mapper)
		{
			_db = db;
			_mapper = mapper;
			_response = new ResponseDto();
		}

		[HttpGet]
		public ResponseDto Get()
		{
			try
			{
				IEnumerable<Coupon> objList = _db.coupons.ToList();
				_response.Result = _mapper.Map<IEnumerable<CouponDto>>(objList);
				_response.isSuccess = true;
				_response.Message = "success";
				return _response;
			}
			catch (Exception ex)
			{
				_response.isSuccess = false;
				_response.Message = ex.Message;
			}
			return _response;
		}

		[HttpGet]
		[Route("{id:int}")]
		public ResponseDto Get(int id)
		{
			try
			{
				Coupon coupon = _db.coupons.First(x => x.CouponId == id);
				_response.Result = _mapper.Map<CouponDto>(coupon);
			}
			catch (Exception ex)
			{
				_response.isSuccess = false;
				_response.Message = ex.Message;

			}

			return _response;
		}

		[HttpGet]
		[Route("GetByCode/{code}")]
		public ResponseDto Get(string code)
		{
			try
			{
				Coupon coupon = _db.coupons.First(x => x.CouponCode.ToLower() == code.ToLower());
				_response.Result = _mapper.Map<CouponDto>(coupon);
			}
			catch (Exception ex)
			{
				_response.isSuccess = false;
				_response.Message = ex.Message;

			}

			return _response;
		}

		[HttpPost]
		public ResponseDto Post([FromBody] CouponDto couponDto)
		{
			try
			{
				Coupon obj = _mapper.Map<Coupon>(couponDto);
				_db.coupons.Add(obj);
				_db.SaveChanges();
				_response.Result = _mapper.Map<CouponDto>(obj);


			}
			catch (Exception ex)
			{
				_response.isSuccess = false;
				_response.Message = ex.Message;

			}
			return _response;
		}

		[HttpPut]
		public ResponseDto Put([FromBody] CouponDto couponDto)
		{
			try
			{
				Coupon obj = _mapper.Map<Coupon>(couponDto);
				_db.coupons.Update(obj);
				_db.SaveChanges();
				_response.Result = _mapper.Map<CouponDto>(obj);


			}
			catch (Exception ex)
			{
				_response.isSuccess = false;
				_response.Message = ex.Message;

			}
			return _response;
		}

		[HttpDelete]
		public ResponseDto Delete(int id)
		{
			try
			{
				Coupon coupon = _db.coupons.First(c=>c.CouponId==id);
				_db.coupons.Remove(coupon);
				_db.SaveChanges();
				_response.Result = _mapper.Map<CouponDto>(coupon);
				_response.Message = "Data Deleted";


			}
			catch (Exception ex)
			{
				_response.isSuccess = false;
				_response.Message = ex.Message;

			}
			return _response;
		}

	}
}
