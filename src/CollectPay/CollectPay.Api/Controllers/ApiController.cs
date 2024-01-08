using ErrorOr;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CollectPay.Api.Controllers;

[ApiController]
public class ApiController : ControllerBase
{
	protected readonly ISender Sender;
	protected readonly IMapper Mapper;

	protected ApiController(ISender sender, IMapper mapper)
	{
		Sender = sender;
		Mapper = mapper;
	}

	protected IActionResult Problem(List<Error> errors)
	{
		return Problem();
	}
}