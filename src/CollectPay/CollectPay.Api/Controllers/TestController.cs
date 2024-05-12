using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CollectPay.Api.Controllers;

public class TestController : ApiController
{
	public TestController(ISender sender)
		: base(sender)
	{
	}

	[HttpGet("/test")]
	public IActionResult Test()
	{
		return Ok();
	}
}