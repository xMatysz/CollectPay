using CollectPay.Application.BillAggregate.Commands.Bills.CreateBill;
using CollectPay.Tests.Integration.Shared;
using ErrorOr;

namespace CollectPay.Application.Tests.Integration.BillAggregatorTests.Commands.Bills;

public class WhenSendingCreateBillCommand : IntegrationTestBase
{
	public WhenSendingCreateBillCommand(WebApiFactory factory)
		: base(factory)
	{
	}

	[Fact]
	public async Task Should_AddBillToRepository()
	{
		var creatorId = Guid.NewGuid();
		const string billName = "BillName";
		var command = new CreateBillCommand(creatorId, billName);

		var result = await Sender.Send(command);
		result.Value.Should().BeEquivalentTo(Result.Created);
		await UnitOfWork.SaveChangesAsync();

		var allBills = await BillRepository.GetAllAsync();
		var bill = allBills.Single();

		bill.CreatorId.Should().Be(creatorId);
		bill.Name.Should().Be(billName);
		bill.Payments.Should().BeEmpty();
	}
}