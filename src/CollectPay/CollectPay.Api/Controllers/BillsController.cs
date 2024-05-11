using CollectionPay.Contracts.Requests.Bill;
using CollectionPay.Contracts.Routes;
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
		var results = await Sender.Send(new GetBillsQuery());

		return QueryResult(results);
	}

	[HttpPost(BillRoutes.Create)]
	public async Task<IActionResult> CreateBill([FromBody] CreateBillRequest request)
	{
		var command = new CreateBillCommand(
			request.UserId,
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
		var command = new RemoveBillCommand(billId);

		var result = await Sender.Send(command);

		return Ok(result);
	}
}
