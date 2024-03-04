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
		HttpContext.Items["errors"] = errors;

		var firstError = errors[0];

		var statusCode = firstError.Type switch
		{
			ErrorType.Conflict => StatusCodes.Status409Conflict,
			ErrorType.Validation => StatusCodes.Status400BadRequest,
			ErrorType.NotFound => StatusCodes.Status404NotFound,
			_ => StatusCodes.Status500InternalServerError
		};

		return Problem(statusCode: statusCode, title: firstError.Description);
	}
}