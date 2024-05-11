using CollectionPay.Contracts.Requests.Bill;
using CollectionPay.Contracts.Routes;
using CollectPay.Api.Authentication;
using CollectPay.Application.BillAggregate.Commands.Bills.CreateBill;
using CollectPay.Application.BillAggregate.Commands.Bills.RemoveBill;
using CollectPay.Application.BillAggregate.Commands.Bills.UpdateBill;
using CollectPay.Application.BillAggregate.Queries.Bills.GetBills;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CollectPay.Api.Controllers;

public class BillsController : ApiController
{
	public BillsController(ISender sender)
		: base(sender)
	{
	}

	[HttpGet(BillRoutes.List)]
	public async Task<IActionResult> GetBills()
	{
		var userId = HttpContext.GetUserId();

		var results = await Sender.Send(new GetBillsQuery(userId!.Value));

		return QueryResult(results);
	}

	[HttpPost(BillRoutes.Create)]
	public async Task<IActionResult> CreateBill([FromBody] CreateBillRequest request)
	{
		var userId = HttpContext.GetUserId();

		var command = new CreateBillCommand(
			userId!.Value,
			request.Name);

		var result = await Sender.Send(command);

		// TODO: fix location
		return result.Match(
			val => Created(BillRoutes.List, val),
			Problem);
	}

	[HttpPut(BillRoutes.Update)]
	public async Task<IActionResult> UpdateBill([FromBody] UpdateBillRequest request)
	{
		var command = new UpdateBillCommand(request.BillId, new UpdateBillInfo(request.Name));

		var result = await Sender.Send(command);

		return Ok(result);
	}

	[HttpDelete(BillRoutes.Remove)]
	public async Task<IActionResult> RemoveBill([FromQuery] Guid billId)
	{
		var userId = HttpContext.GetUserId();

		if (userId is null)
		{
			return Problem(statusCode: 500, detail: "UserId cannot be taken from claims");
		}

		var command = new RemoveBillCommand(userId!.Value, billId);

		var result = await Sender.Send(command);

		return result.Match(
			_ => Ok(),
			Problem);
	}
}
