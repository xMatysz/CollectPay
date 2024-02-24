using CollectionPay.Contracts.Requests.Bill;
using CollectionPay.Contracts.Routes;
using CollectPay.Application.BillAggregate.Commands.Bill.CreateBill;
using CollectPay.Application.BillAggregate.Queries.GetBills;
using CollectPay.Domain.BillAggregate;
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
	public async Task<List<Bill>> GetBills()
	{
		var results = await Sender.Send(new GetBillsQuery());
		return results.Value;
	}

	[HttpPost(BillRoutes.Create)]
	public async Task CreateBill([FromBody] CreateBillRequest request)
	{
		var command = new CreateBillCommand(
			request.UserId,
			request.Name);

		var result = await Sender.Send(command);

		result.Match(
			created => Ok(created),
			Problem);
	}
}