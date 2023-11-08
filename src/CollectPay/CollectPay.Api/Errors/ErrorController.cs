using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace CollectPay.Api.Errors;

public class ErrorController : ControllerBase
{
	[Route("/error")]
	public IActionResult Error()
	{
		var exception = HttpContext.Features.Get<IExceptionHandlerFeature>()?.Error;

		return Problem();
	}
}