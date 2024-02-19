using CollectionPay.Contracts.Requests;
using CollectionPay.Contracts.Responses;
using CollectionPay.Contracts.Routes;
using CollectPay.Application.UserAggregate.Commands.CreateUser;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CollectPay.Api.Controllers;

public class UserController : ApiController
{
	protected UserController(ISender sender, IMapper mapper)
		: base(sender, mapper)
	{
	}

	[HttpPost(UserRoutes.Create)]
	public async Task Create([FromBody] CreateUserRequest request, CancellationToken cancellationToken)
	{
		var command = Mapper.Map<CreateUserCommand>(request);

		var result = await Sender.Send(command, cancellationToken);

		result.Match(commandResult => Ok(Mapper.Map<CreateUserResponse>(commandResult)), Problem);
	}
}