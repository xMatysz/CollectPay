using CollectPay.Application.BillAggregate.Commands.Bills.RemoveBill;
using CollectPay.Tests.Integration.Shared;

namespace CollectPay.Application.Tests.Integration.BillAggregatorTests.Commands.Bills;

public class WhenSendingRemoveBillCommand : IntegrationTestBase
{
	public WhenSendingRemoveBillCommand(WebApiFactory factory)
		: base(factory)
	{
	}

	[Fact]
	public async Task ShouldRemoveBillFromRepository()
	{
		var userId = Guid.NewGuid();
		var bill = new BillBuilder().WithCreatorId(userId).Build();
		AssumeEntityInDb(bill);
		var request = new RemoveBillCommand(userId, bill.Id);

		await Sender.Send(request);

		var result = await BillRepository.GetByIdAsync(bill.Id);

		result.Should().BeNull();
	}
}