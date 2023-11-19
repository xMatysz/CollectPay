﻿using CollectPay.Api.Common;
using CollectPay.Application.BillAggregate.Commands.Create;
using CollectPay.Application.BillAggregate.Queries;
using CollectPay.Domain.BillAggregate;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CollectPay.Api.Controllers;

public class BillsController : ApiController
{
	public BillsController(IMediator mediator)
		: base(mediator)
	{
	}

	[HttpGet(BillRouters.List)]
	public async Task<List<Bill>> GetBills()
	{
		var results = await Mediator.Send(new GetBillsQuery());
		return results;
	}

	[HttpPost(BillRouters.Create)]
	public async Task CreateBill([FromBody] CreateBillCommand command)
	{
		await Mediator.Send(command);
	}
}