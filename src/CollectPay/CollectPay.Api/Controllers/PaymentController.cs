using CollectionPay.Contracts.Requests.Bill;
using CollectionPay.Contracts.Requests.Payment;
using CollectionPay.Contracts.Routes;
using CollectPay.Application.BillAggregate.Commands.Payments.CreatePayment;
using CollectPay.Application.BillAggregate.Commands.Payments.RemovePayment;
using CollectPay.Application.BillAggregate.Commands.Payments.UpdatePayment;
using CollectPay.Application.BillAggregate.Queries.GetPayments;
using CollectPay.Domain.BillAggregate.ValueObjects;
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
	public async Task<IActionResult> GetPayments([FromQuery] Guid billId)
	{
		var query = new GetPaymentsQuery(billId);

		var result = await Sender.Send(query);
		return QueryResult(result);
	}

	[HttpPost(PaymentRoutes.Create)]
	public async Task<IActionResult> CreatePayment([FromBody] CreatePaymentRequest request)
	{
		var command = new CreatePaymentCommand(
			request.BillId,
			request.CreatorId,
			request.IsCreatorIncluded,
			Amount.Create(request.Amount, request.Currency).Value,
			request.Debtors);

		var result = await Sender.Send(command);
		return CreateResult(result);
	}

	[HttpPut(PaymentRoutes.Update)]
	public async Task<IActionResult> UpdatePayment([FromBody] UpdatePaymentRequest request)
	{
		var command = new UpdatePaymentCommand(request.BillId);

		var result = await Sender.Send(command);
		return Ok(result);
	}

	[HttpDelete(PaymentRoutes.Remove)]
	public async Task<IActionResult> RemovePayments([FromQuery] Guid billId)
	{
		var command = new RemovePaymentCommand(billId);

		var result = await Sender.Send(command);
		return Ok(result);
	}
}