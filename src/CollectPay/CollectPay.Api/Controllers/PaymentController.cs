using CollectionPay.Contracts;
using CollectionPay.Contracts.Requests.Payment;
using CollectionPay.Contracts.Responses;
using CollectPay.Api.Authentication;
using CollectPay.Application.BillAggregate.Commands.Payments.CreatePayment;
using CollectPay.Application.BillAggregate.Commands.Payments.RemovePayment;
using CollectPay.Application.BillAggregate.Commands.Payments.UpdatePayment;
using CollectPay.Application.BillAggregate.Queries.Payments.GetPayments;
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
		var query = new GetPaymentsQuery(HttpContext.GetUserId(), billId);

		var result = await Sender.Send(query);

		return result.Match(
			list => Ok(list.Select(payment => new GetPaymentsResponse(payment.Id, payment.Name,  payment.Amount.Value, payment.Amount.Currency))),
			Problem);
	}

	[HttpPost(PaymentRoutes.Create)]
	public async Task<IActionResult> CreatePayment([FromBody] CreatePaymentRequest request)
	{
		var command = new CreatePaymentCommand(
			request.Name,
			request.BillId,
			HttpContext.GetUserId(),
			Amount.Create(request.Amount, request.Currency).Value,
			request.Debtors);

		var result = await Sender.Send(command);

		// TODO: fix location
		return result.Match(
			val => Created(PaymentRoutes.List, val),
			Problem);

	}

	[HttpPut(PaymentRoutes.Update)]
	public async Task<IActionResult> UpdatePayment([FromBody] UpdatePaymentRequest request)
	{
		var amount = request.Amount is not null && request.Currency is not null
			? Amount.Create(request.Amount.Value, request.Currency).Value
			: null;

		var command = new UpdatePaymentCommand(
			HttpContext.GetUserId(),
			request.BillId,
			request.PaymentId,
			new UpdatePaymentInfo(
				request.Name,
				amount,
				request.DebtorsToAdd,
				request.DebtorsToRemove));

		var result = await Sender.Send(command);
		return Ok(result);
	}

	[HttpDelete(PaymentRoutes.Remove)]
	public async Task<IActionResult> RemovePayments([FromQuery] Guid billId, Guid paymentId)
	{
		var command = new RemovePaymentCommand(HttpContext.GetUserId(), billId, paymentId);

		var result = await Sender.Send(command);
		return Ok(result);
	}
}