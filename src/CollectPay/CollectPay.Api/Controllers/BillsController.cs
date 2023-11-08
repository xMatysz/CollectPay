using CollectPay.Api.Common;
using CollectPay.Application.BillAggregate.Commands.Create;
using CollectPay.Application.BillAggregate.Queries;
using CollectPay.Domain.BillAggregate;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CollectPay.Api.Controllers;

[ApiController]
public class BillsController
{
	private readonly IMediator _mediator;

	public BillsController(IMediator mediator)
	{
		_mediator = mediator;
	}

	[HttpGet(Routes.Bills)]
	public async Task<List<Bill>> GetBills()
	{
		var results = await _mediator.Send(new GetBillsQuery());
		return results;
	}

	[HttpPost(Routes.BillCreate)]
	public async Task CreateBill([FromBody] CreateBillCommand command)
	{
		await _mediator.Send(command);
	}
}