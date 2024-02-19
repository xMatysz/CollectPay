using CollectionPay.Contracts.Routes;
using CollectPay.Application.BillAggregate.Queries.GetPayments;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CollectPay.Api.Controllers;

public class PaymentController : ApiController
{
	protected PaymentController(ISender sender, IMapper mapper)
		: base(sender, mapper)
	{
	}

	[HttpGet(PaymentRoutes.List)]
	public IActionResult GetPayments([FromQuery] Guid billId)
	{
		var query = new GetPaymentsQuery(billId);

		var result = Sender.Send(query);
		return Ok(result);
	}
}