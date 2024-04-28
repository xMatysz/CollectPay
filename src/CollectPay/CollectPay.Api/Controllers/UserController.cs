using CollectionPay.Contracts.Requests.User;
using CollectionPay.Contracts.Routes;
using CollectPay.Application.UserAggregate.Register;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CollectPay.Api.Controllers;

public class UserController : ApiController
{
	public UserController(ISender sender)
		: base(sender)
	{
	}

	[HttpPost(UserRoutes.Register)]
	public async Task<IActionResult> RegisterUser(RegisterUserRequest request)
	{
		var command = new RegisterUserCommand(request.Email, request.Password);

		var result = await Sender.Send(command);

		return result.Match(
			_ => Ok(),
			Problem);
	}
}