using CollectionPay.Contracts.Requests;
using CollectionPay.Contracts.Routes;
using CollectPay.Application.BillAggregate.Commands.Create;
using CollectPay.Application.BillAggregate.Queries;
using CollectPay.Domain.BillAggregate;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CollectPay.Api.Controllers;

public class BillsController : ApiController
{
	public BillsController(ISender sender, IMapper mapper)
		: base(sender, mapper)
	{
	}

	[HttpGet(BillRoutes.List)]
	public async Task<List<Bill>> GetBills()
	{
		var results = await Sender.Send(new GetBillsQuery());
		return results;
	}

	[HttpPost(BillRoutes.Create)]
	public async Task CreateBill([FromBody] CreateBillRequest request)
	{
		var command = Mapper.Map<CreateBillCommand>(request);

		var result = await Sender.Send(command);

		result.Match(
			created => Ok(created),
			Problem);
	}
}