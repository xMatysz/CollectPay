using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CollectPay.Api.Controllers;

[ApiController]
public class ApiController
{
	protected readonly IMediator Mediator;

	protected ApiController(IMediator mediator)
	{
		Mediator = mediator;
	}
}