using CollectPay.Application.BillAggregate.Commands.Payments.UpdatePayment;
using CollectPay.Domain.BillAggregate.Errors;
using CollectPay.Domain.BillAggregate.ValueObjects;
using CollectPay.Tests.Shared.Builders;
using ErrorOr;

namespace CollectPay.Application.UnitTests.BillAggregateTests.Payments;

public class WhenHandlingUpdatePaymentCommand : UnitTestBase
{
	private readonly UpdatePaymentCommandHandler _handler;

	public WhenHandlingUpdatePaymentCommand()
	{
		_handler = new UpdatePaymentCommandHandler(BillRepository);
	}

	[Fact]
	public async Task ShouldFailWhenBillNotExist()
	{
		var command = new UpdatePaymentCommand(Guid.NewGuid(), Guid.NewGuid(), null!);

		var result = await _handler.Handle(command);

		result.IsError.Should().BeTrue();
		result.FirstError.Should().Be(BillErrors.BillNotFound);
	}

	[Fact]
	public async Task ShouldFailWhenPaymentNotExist()
	{
		var bill = new BillBuilder().Build();
		BillRepository.GetByIdAsync(bill.Id).Returns(bill);

		var command = new UpdatePaymentCommand(bill.Id, Guid.NewGuid(), null!);

		var result = await _handler.Handle(command);

		result.IsError.Should().BeTrue();
		result.FirstError.Should().Be(PaymentErrors.PaymentNotFound);
	}

	[Fact]
	public async Task ShouldNotUpdateWhenAnyValueIsSpecified()
	{
		var bill = new BillBuilder().Build();
		var payment = new PaymentBuilder().WithBillId(bill.Id).Build();
		bill.AddPayment(payment);
		BillRepository.GetByIdAsync(bill.Id).Returns(bill);

		var command = new UpdatePaymentCommand(bill.Id,
			payment.Id,
			new UpdatePaymentInfo(
				null,
				null,
				null,
				null));

		var result = await _handler.Handle(command);

		result.IsError.Should().BeFalse();
		result.Value.Should().Be(Result.Updated);
		bill.Payments.First().Should().BeEquivalentTo(payment);
	}

	[Fact]
	public async Task ShouldFailWhenUpdateCreateError()
	{
		var bill = new BillBuilder().Build();
		var creator = Guid.NewGuid();
		var payment = new PaymentBuilder()
			.WithBillId(bill.Id)
			.WithCreatorId(creator)
			.Build();
		bill.AddPayment(payment);
		BillRepository.GetByIdAsync(bill.Id).Returns(bill);

		var command = new UpdatePaymentCommand(bill.Id,
			payment.Id,
			new UpdatePaymentInfo(
				null,
				null,
				null,
				[creator]));

		var result = await _handler.Handle(command);

		result.IsError.Should().BeTrue();
		result.FirstError.Should().Be(PaymentErrors.CreatorCannotBeDebtor);
	}

	[Fact]
	public async Task ShouldUpdateAllFieldsCorrectly()
	{
		var bill = new BillBuilder().Build();
		var payment = new PaymentBuilder()
			.WithBillId(bill.Id)
			.Build();
		bill.AddPayment(payment);
		BillRepository.GetByIdAsync(bill.Id).Returns(bill);

		var newCreator = Guid.NewGuid();
		var isCreatorIncluded = !payment.IsCreatorIncluded;
		var amount = Amount.Create(11.11m, "PLN").Value;
		Guid[] debtors = [Guid.NewGuid(), Guid.NewGuid()];

		var command = new UpdatePaymentCommand(bill.Id,
			payment.Id,
			new UpdatePaymentInfo(
				newCreator,
				isCreatorIncluded,
				amount,
				debtors));

		var result = await _handler.Handle(command);

		result.IsError.Should().BeFalse();
		bill.Payments.First().Id.Should().Be(payment.Id);
		bill.Payments.First().BillId.Should().Be(payment.BillId);
		bill.Payments.First().CreatorId.Should().Be(newCreator);
		bill.Payments.First().Amount.Should().Be(amount);
		bill.Payments.First().IsCreatorIncluded.Should().Be(isCreatorIncluded);
		bill.Payments.First().DebtorIds.Should().BeEquivalentTo(debtors);
	}
}