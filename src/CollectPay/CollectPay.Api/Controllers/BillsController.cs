using CollectionPay.Contracts.Requests.Bill;
using CollectionPay.Contracts.Routes;
using CollectPay.Application.BillAggregate.Commands.Bills.CreateBill;
using CollectPay.Application.BillAggregate.Queries.GetBills;
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

		return CreateResult(result);
	}
}