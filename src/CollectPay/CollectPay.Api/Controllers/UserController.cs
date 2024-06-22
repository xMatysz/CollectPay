using CollectionPay.Contracts;
using CollectionPay.Contracts.Requests.User;
using CollectionPay.Contracts.Responses;
using CollectPay.Application.UserAggregate.Login;
using CollectPay.Application.UserAggregate.Register;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CollectPay.Api.Controllers;

public class UserController : ApiController
{
	public UserController(ISender sender)
		: base(sender)
	{
	}

	[AllowAnonymous]
	[HttpPost(UserRoutes.Register)]
	public async Task<IActionResult> RegisterUser(RegisterUserRequest request)
	{
		var command = new RegisterUserCommand(request.Email, request.Password);

		var result = await Sender.Send(command);

		return result.Match(
			_ => Ok(),
			Problem);
	}

	[AllowAnonymous]
	[HttpPost(UserRoutes.Login)]
	public async Task<IActionResult> LoginUser(LoginUserRequest request)
	{
		var query = new LoginUserQuery(request.Email, request.Password);

		var result = await Sender.Send(query);

		return result.Match(
			val => Ok(new LoginUserResponse(val.Token, val.Email)),
			Problem);
	}
}