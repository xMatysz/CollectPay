using CollectPay.Application.BillAggregate.Commands.Payments.CreatePayment;
using CollectPay.Application.Common.Repositories;
using CollectPay.Domain.BillAggregate.ValueObjects;
using CollectPay.Tests.Shared.Builders;

namespace CollectPay.Application.UnitTests.BillAggregateTests.Payments;

public class WhenHandlingCreatePaymentCommand
{
	private readonly CreatePaymentCommandHandler _handler;
	private readonly IBillRepository _billRepo;

	public WhenHandlingCreatePaymentCommand()
	{
		_billRepo = Substitute.For<IBillRepository>();
		_handler = new CreatePaymentCommandHandler(_billRepo);
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
		await _billRepo.DidNotReceive().GetByIdAsync(bill.Id);
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
		await _billRepo.Received(1).GetByIdAsync(Arg.Any<Guid>());
	}
}