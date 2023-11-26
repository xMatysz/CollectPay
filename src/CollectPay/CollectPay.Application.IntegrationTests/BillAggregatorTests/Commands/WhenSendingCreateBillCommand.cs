using CollectPay.Application.BillAggregate.Commands.Create;
using ErrorOr;

namespace CollectPay.Application.IntegrationTests.BillAggregatorTests.Commands;

public class WhenSendingCreateBillCommand : IntegrationTestBase, IClassFixture<WebApiFactory>
{
	private readonly CreateBillCommandHandler _handler;

	public WhenSendingCreateBillCommand(WebApiFactory factory)
		: base(factory)
	{
		_handler = new CreateBillCommandHandler(BillRepository);
	}

	[Fact]
	public async Task ShouldAddBillToDb()
	{
		var creatorId = Guid.NewGuid();
		const string billName = "BillName";
		var command = new CreateBillCommand(creatorId, billName, null);

		var result = await _handler.Handle(command, CancellationToken.None);
		result.Should().Be(Result.Created);
		await UnitOfWork.SaveChangesAsync();

		var allBills = await BillRepository.GetAllAsync();
		var bill = allBills.Single();

		bill.CreatorId.Should().Be(creatorId);
		bill.Name.Should().Be(billName);
		bill.Payments.Should().BeEmpty();
	}
}