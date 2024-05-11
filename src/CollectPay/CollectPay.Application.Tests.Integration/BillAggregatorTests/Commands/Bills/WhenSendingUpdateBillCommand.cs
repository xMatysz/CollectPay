using CollectPay.Application.BillAggregate.Commands.Bills.UpdateBill;
using CollectPay.Tests.Integration.Shared;

namespace CollectPay.Application.Tests.Integration.BillAggregatorTests.Commands.Bills;

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
		var userId = Guid.NewGuid();
		var bill = new BillBuilder()
			.WithName(oldName)
			.WithCreatorId(userId)
			.Build();
		AssumeEntityInDb(bill);
		var request = new UpdateBillCommand(bill.Id, userId, new UpdateBillInfo(newName));

		await Sender.Send(request);
		var billFromDb = await BillRepository.GetByIdAsync(bill.Id);

		billFromDb!.Name.Should().Be(newName);
	}
}