using CollectionPay.Contracts.Routes;
using CollectPay.Application.BillAggregate.Queries.GetPayments;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CollectPay.Api.Controllers;

public class PaymentController : ApiController
{
	public PaymentController(ISender sender)
		: base(sender)
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