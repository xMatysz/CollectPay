using CollectPay.Application.BillAggregate.Commands.Bills.RemoveBill;

namespace CollectPay.Application.IntegrationTests.BillAggregatorTests.Commands.Bills;

public class WhenSendingRemoveBillCommand : IntegrationTestBase
{
	public WhenSendingRemoveBillCommand(WebApiFactory factory)
		: base(factory)
	{
	}

	[Fact]
	public async Task ShouldRemoveBillFromRepository()
	{
		var bill = new BillBuilder().Build();
		AssumeEntityInDb(bill);
		var request = new RemoveBillCommand(bill.Id);

		await Sender.Send(request);

		var result = await BillRepository.GetByIdAsync(bill.Id);

		result.Should().BeNull();
	}
}