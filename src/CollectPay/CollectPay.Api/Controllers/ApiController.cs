using System.Runtime.CompilerServices;
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

	protected IActionResult QueryResult<T>(ErrorOr<T> results)
	{
		return results.Match(
			val => Ok(val),
			Problem);
	}

	protected IActionResult CreateResult<T>(ErrorOr<T> result, [CallerMemberName] string callerName = "")
	{
		return result.Match(
			val => CreatedAtAction(callerName, val),
			Problem);
	}

	protected IActionResult Problem(List<Error> errors)
	{
		return Problem();
	}
}