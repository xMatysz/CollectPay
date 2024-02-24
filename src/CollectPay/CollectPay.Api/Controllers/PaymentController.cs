using CollectionPay.Contracts.Requests.Bill;
using CollectionPay.Contracts.Requests.Payment;
using CollectionPay.Contracts.Routes;
using CollectPay.Application.BillAggregate.Commands.Payment.CreatePayment;
using CollectPay.Application.BillAggregate.Commands.Payment.RemovePayment;
using CollectPay.Application.BillAggregate.Commands.Payment.UpdatePayment;
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

	[HttpPost(PaymentRoutes.Create)]
	public IActionResult CreatePayment([FromBody] CreatePaymentRequest request)
	{
		var command = new CreatePaymentCommand(request.BillId);

		var result = Sender.Send(command);
		return Ok(result);
	}

	[HttpPut(PaymentRoutes.Update)]
	public IActionResult UpdatePayment([FromBody] UpdatePaymentRequest request)
	{
		var command = new UpdatePaymentCommand(request.BillId);

		var result = Sender.Send(command);
		return Ok(result);
	}

	[HttpDelete(PaymentRoutes.Remove)]
	public IActionResult RemovePayments([FromQuery] Guid billId)
	{
		var command = new RemovePaymentCommand(billId);

		var result = Sender.Send(command);
		return Ok(result);
	}
}