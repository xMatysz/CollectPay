using CollectPay.Application.BillAggregate.Commands.Payments.CreatePayment;
using CollectPay.Domain.BillAggregate.ValueObjects;

namespace CollectPay.Application.Tests.Unit.BillAggregateTests.Payments;

public class WhenHandlingCreatePaymentCommand : UnitTestBase
{
	private readonly CreatePaymentCommandHandler _handler;

	public WhenHandlingCreatePaymentCommand()
	{
		_handler = new CreatePaymentCommandHandler(BillRepository);
	}

	[Fact]
	public async Task ShouldFailWhenPaymentCreationFail()
	{
		var bill = new BillBuilder().Build();

		var creator = Guid.NewGuid();
		var command = new CreatePaymentCommand(
			bill.Id,
			creator,
			true,
			Amount.Create(21.37m, "USD").Value,
			[creator, Guid.NewGuid()]);

		var result = await _handler.Handle(command);

		result.IsError.Should().BeTrue();
		await BillRepository.DidNotReceive().GetByIdAsync(bill.Id);
	}

	[Fact]
	public async Task ShouldFailWhenBillNotExist()
	{
		var creator = Guid.NewGuid();
		var command = new CreatePaymentCommand(
			Guid.NewGuid(),
			creator,
			true,
			Amount.Create(21.37m, "USD").Value,
			[Guid.NewGuid()]);

		var result = await _handler.Handle(command);

		result.IsError.Should().BeTrue();
		await BillRepository.Received(1).GetByIdAsync(Arg.Any<Guid>());
	}
}