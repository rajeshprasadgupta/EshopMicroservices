using Discount.Grpc.Data;
using Discount.Grpc.Model;
using Grpc.Core;
using Mapster;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;

namespace Discount.Grpc.Services
{
	public class DiscountService
		(DiscountContext dbcontext, ILogger<DiscountService> logger)
		: DiscountProtoService.DiscountProtoServiceBase
	{
		public override async Task<CouponModel> GetDiscount(GetDiscountRequest request, ServerCallContext context)
		{
			var coupon = await dbcontext.Coupons.
				FirstOrDefaultAsync(c => c.ProductName == request.ProductName);
			if (coupon == null)
			{
				logger.LogInformation("Discount is not found for ProductName : {ProductName}, returning empty product", request.ProductName);
				coupon = new Coupon { ProductName = "No Discount", Amount = 0, Description = "No Discount Desc" };
			}
			else
			{
				logger.LogInformation("Discount is retrieved for ProductName : {ProductName}, Amount : {Amount}", coupon.ProductName, coupon.Amount);
			}
			return coupon.Adapt<CouponModel>();
		}

		public override async Task<CouponModel> CreateDiscount(CreateDiscountRequest request, ServerCallContext context)
		{
			var coupon = request.Coupon.Adapt<Coupon>();
			if(coupon == null || string.IsNullOrEmpty(coupon.ProductName))
			{
				throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid Request"));
			}
			dbcontext.Coupons.Add(coupon);
			var result = await dbcontext.SaveChangesAsync();
			if(result == 0)
			{
				throw new RpcException(new Status(StatusCode.Internal, "Failed to create Discount"));
			}
			logger.LogInformation("Discount is successfully created. ProductName : {ProductName}", coupon.ProductName);
			return coupon.Adapt<CouponModel>();
		}

		public override async Task<CouponModel> UpdateDiscount(UpdateDiscountRequest request, ServerCallContext context)
		{
			var couponrequest = request.Coupon.Adapt<Coupon>();
			var coupon = await dbcontext.Coupons.
				FirstOrDefaultAsync(c => c.Id == couponrequest.Id);
			if(coupon == null)
			{
				throw new RpcException(new Status(StatusCode.NotFound, $"Discount with ProductId={request.Coupon.Id} not found"));
			}
			coupon.Amount = couponrequest.Amount;
			coupon.ProductName = couponrequest.ProductName;
			coupon.Description = couponrequest.Description;
			var result = await dbcontext.SaveChangesAsync();
			if (result == 0)
			{
				throw new RpcException(new Status(StatusCode.Internal, "Failed to update Discount"));
			}
			logger.LogInformation("Discount is successfully updated. ProductName : {ProductName}", coupon.ProductName);
			return coupon.Adapt<CouponModel>();
		}

		public override async Task<DeleteDiscountResponse> DeleteDiscount(DeleteDiscountRequest request, ServerCallContext context)
		{
			var coupon = await dbcontext.Coupons.
				FirstOrDefaultAsync(c => c.Id == request.Id);
			if (coupon == null)
			{
				throw new RpcException(new Status(StatusCode.NotFound, $"Discount with ProductId={request.Id} not found"));
			}
			dbcontext.Coupons.Remove(coupon);
			var result = await dbcontext.SaveChangesAsync();
			if (result == 0)
			{
				throw new RpcException(new Status(StatusCode.Internal, "Failed to delete Discount"));
			}
			logger.LogInformation("Discount is successfully deleted. ProductName : {ProductName}", coupon.ProductName);
			return new DeleteDiscountResponse { Success = result > 0 };
		}
	}
}
