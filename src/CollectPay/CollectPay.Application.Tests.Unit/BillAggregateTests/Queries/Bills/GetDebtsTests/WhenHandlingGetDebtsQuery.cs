using CollectPay.Application.BillAggregate.Queries.Bills.GetDebts;
using CollectPay.Application.Services;
using CollectPay.Domain.BillAggregate.Errors;

namespace CollectPay.Application.Tests.Unit.BillAggregateTests.Queries.Bills.GetDebtsTests;

public class WhenHandlingGetDebtsQuery : UnitTestBase
{
	private readonly  GetDebtsQueryHandler _handler;

	public WhenHandlingGetDebtsQuery()
	{
		var service = Substitute.For<IDebtService>();
		 _handler = new GetDebtsQueryHandler(BillRepository, service);
	}

	[Fact]
	public async Task Should_Fail_WhenBillNotExist()
	{
		var query = new GetDebtsQuery(Guid.NewGuid(), Guid.NewGuid());

		var result = await _handler.Handle(query,CancellationToken.None);

		result.IsError.Should().BeTrue();
		result.FirstError.Should().Be(BillErrors.BillNotFound);
	}

	[Fact]
	public async Task Should_Fail_WhenUserIsNotAttached()
	{
		BillRepository.GetByIdAsync(Arg.Any<Guid>())
			.ReturnsForAnyArgs(BillBuilder.Default().Build());
		var query = new GetDebtsQuery(Guid.NewGuid(), Guid.NewGuid());

		var result = await _handler.Handle(query,CancellationToken.None);

		result.IsError.Should().BeTrue();
		result.FirstError.Should().Be(BillErrors.BillNotFound);
	}

	[Fact]
	public async Task Should_Return_WhenUserIsAttached()
	{
		var debtor = Guid.NewGuid();
		var bill = BillBuilder.Default().WithDebtors([debtor]).Build();
		BillRepository.GetByIdAsync(Arg.Any<Guid>())
			.ReturnsForAnyArgs(bill);
		var query = new GetDebtsQuery(debtor, Guid.NewGuid());

		var result = await _handler.Handle(query,CancellationToken.None);

		result.IsError.Should().BeFalse();
	}
}