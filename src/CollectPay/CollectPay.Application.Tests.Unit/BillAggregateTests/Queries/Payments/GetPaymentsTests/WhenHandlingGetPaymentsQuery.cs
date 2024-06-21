using CollectPay.Application.BillAggregate.Queries.Payments.GetPayments;
using CollectPay.Domain.BillAggregate.Errors;

namespace CollectPay.Application.Tests.Unit.BillAggregateTests.Queries.Payments.GetPaymentsTests;

public class WhenHandlingGetPaymentsQuery : UnitTestBase
{
	private readonly GetPaymentsQueryHandler _handler;

	public WhenHandlingGetPaymentsQuery()
	{
		_handler = new GetPaymentsQueryHandler(BillRepository);
	}

	[Fact]
	public async Task Should_Fail_WhenBillNotExist()
	{
		var query = new GetPaymentsQuery(Guid.NewGuid(), Guid.NewGuid());

		var result = await _handler.Handle(query,CancellationToken.None);

		result.IsError.Should().BeTrue();
		result.FirstError.Should().Be(BillErrors.BillNotFound);
	}

	[Fact]
	public async Task Should_Fail_WhenUserIsNotAttached()
	{
		BillRepository.GetByIdAsync(Arg.Any<Guid>())
			.ReturnsForAnyArgs(BillBuilder.Default().Build());
		var query = new GetPaymentsQuery(Guid.NewGuid(), Guid.NewGuid());

		var result = await _handler.Handle(query,CancellationToken.None);

		result.IsError.Should().BeTrue();
		result.FirstError.Should().Be(BillErrors.BillNotFound);
	}
}