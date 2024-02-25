﻿using CollectPay.Application.BillAggregate.Commands.Bills.CreateBill;
using CollectPay.Domain.BillAggregate;
using CollectPay.Tests.Shared.Builders;

namespace CollectPay.Application.UnitTests.BillAggregateTests.Bills;

public class WhenHandlingCreateBillCommand : UnitTestBase
{
	private readonly CreateBillCommandHandler _handler;

	public WhenHandlingCreateBillCommand()
	{
		_handler = new CreateBillCommandHandler(BillRepository);
	}

	[Fact]
	public async Task ShouldAddBillToRepository()
	{
		var creator = Guid.NewGuid();
		var billName = BillBuilder.TestName;

		var command = new CreateBillCommand(creator, billName);
		await _handler.Handle(command);

		await BillRepository
			.Received(1)
			.AddAsync(Arg.Is<Bill>(bill =>
				bill.CreatorId == creator &&
				bill.Name == billName));
	}
}

