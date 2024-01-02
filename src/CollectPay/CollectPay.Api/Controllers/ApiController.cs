using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CollectPay.Api.Controllers;

[ApiController]
public class ApiController : ControllerBase
{
	protected readonly ISender Sender;

	protected ApiController(ISender sender)
	{
		Sender = sender;
	}

	protected IActionResult Problem(List<Error> errors)
	{
		return Problem();
	}
}