using System.Security.Claims;

namespace CollectPay.Api.Authentication;

public static class IdentityExtensions
{
	public static Guid? GetUserId(this HttpContext context)
	{
		var userId = context.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Sid);

		return Guid.TryParse(userId?.Value, out var parsedId)
			? parsedId
			: null;
	}
}