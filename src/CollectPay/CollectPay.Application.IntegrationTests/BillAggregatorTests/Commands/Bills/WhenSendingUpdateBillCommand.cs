﻿using CollectionPay.Contracts.Requests.Bill;
using CollectPay.Application.BillAggregate.Commands.Bills.UpdateBill;

namespace CollectPay.Application.IntegrationTests.BillAggregatorTests.Commands.Bills;

public class WhenSendingUpdateBillCommand : IntegrationTestBase
{
	public WhenSendingUpdateBillCommand(WebApiFactory factory)
		: base(factory)
	{
	}

	[Fact]
	public async Task ShouldUpdateBill()
	{
		const string oldName = "Bill1";
		const string newName = "Bill2";
		var bill = new BillBuilder()
			.WithName(oldName)
			.Build();
		AssumeEntityInDb(bill);
		var request = new UpdateBillCommand(bill.Id, new UpdateBillInfo(newName));

		await Sender.Send(request);
		var billFromDb = await BillRepository.GetByIdAsync(bill.Id);

		billFromDb!.Name.Should().Be(newName);
	}
}