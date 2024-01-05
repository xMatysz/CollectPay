using CollectionPay.Contracts.Routes;
using CollectPay.Application.BillAggregate.Commands.Create;
using CollectPay.Application.BillAggregate.Queries;
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
		return results;
	}

	[HttpPost(BillRoutes.Create)]
	public async Task CreateBill([FromBody] CreateBillCommand command)
	{
		var result = await Sender.Send(command);

		result.Match(
			created => Ok(created),
			Problem);
	}
}