using CollectPay.Application.BillAggregate.Commands.Payments.CreatePayment;
using CollectPay.Domain.BillAggregate;
using CollectPay.Domain.BillAggregate.Errors;

namespace CollectPay.Application.Tests.Unit.BillAggregateTests.Commands.Payments.CreatePaymentTests;

public class WhenHandlingCreatePaymentCommand : UnitTestBase
{
	private readonly CreatePaymentCommandHandler _handler;

	public WhenHandlingCreatePaymentCommand()
	{
		_handler = new CreatePaymentCommandHandler(BillRepository);
	}

	[Fact]
	public async Task Should_Fail_WhenBillNotExist()
	{
		var command = GetCommand();

		var result = await _handler.Handle(command);

		result.IsError.Should().BeTrue();
		result.FirstError.Should().Be(BillErrors.BillNotFound);
	}


	[Fact]
	public async Task Should_Fail_WhenCreatorIsNotAttachedToBill()
	{
		BillRepository.GetByIdAsync(Arg.Any<Guid>())
			.Returns(BillBuilder.Default().Build());

		var command = GetCommand(billId: Guid.NewGuid());

		var result = await _handler.Handle(command);

		result.IsError.Should().BeTrue();
		result.FirstError.Should().Be(BillErrors.BillNotFound);
	}

	[Fact]
	public async Task Should_CreatePaymentAndAttachToBill()
	{
		var paymentCreatorId = Guid.NewGuid();
		var bill = AssumeBillWithDebtor(paymentCreatorId);
		BillRepository
			.GetByIdAsync(Arg.Any<Guid>())
			.Returns(bill);

		var command = GetCommand(billId: bill.Id, creatorId: paymentCreatorId);

		var result = await _handler.Handle(command);

		result.IsError.Should().BeFalse();
		result.Value.Should().Be(Result.Created);
		bill.Payments.Should().HaveCount(1);
	}

	private static Bill AssumeBillWithDebtor(Guid debtorId)
	{
		var bill = BillBuilder.Default().Build();
		bill.Update(BillBuilder.DefaultName, [debtorId], []);
		return bill;
	}

	private CreatePaymentCommand GetCommand(Guid? billId = null, Guid? creatorId = null)
	{
		return new CreatePaymentCommand(
			PaymentBuilder.DefaultName,
			billId ?? Guid.NewGuid(),
			creatorId ?? Guid.NewGuid(),
			PaymentBuilder.DefaultAmount,
			[]);
	}
}