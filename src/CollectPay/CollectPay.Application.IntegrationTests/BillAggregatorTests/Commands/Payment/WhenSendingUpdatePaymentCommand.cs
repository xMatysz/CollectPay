using CollectPay.Application.BillAggregate.Commands.Payments.UpdatePayment;
using CollectPay.Domain.BillAggregate.ValueObjects;

namespace CollectPay.Application.IntegrationTests.BillAggregatorTests.Commands.Payment;

public class WhenSendingUpdatePaymentCommand : SendPaymentCommandBase
{
	public WhenSendingUpdatePaymentCommand(WebApiFactory factory)
		: base(factory)
	{
	}

	[Fact]
	public async Task ShouldUpdatePayment()
	{
		var bill = await BillRepository.GetByIdAsync(Bill.Id);
		var payment = new PaymentBuilder().Build();
		bill!.AddPayment(payment);
		await UnitOfWork.SaveChangesAsync();

		var newCreator = Guid.NewGuid();
		var newIsCreatorIncluded = !payment.IsCreatorIncluded;
		var newAmount = Amount.Create(11.11m, "PLN").Value;
		Guid[] newDebtors = [Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid()];

		var command = new UpdatePaymentCommand(Bill.Id,
			payment.Id,
			new UpdatePaymentInfo(
				newCreator,
				newIsCreatorIncluded,
				newAmount,
				newDebtors));

		await Sender.Send(command);

		var billFromDb = await BillRepository.GetByIdAsync(Bill.Id);

		var updatedPayment = billFromDb!.Payments.First();

		updatedPayment.Id.Should().Be(payment.Id);
		updatedPayment.BillId.Should().Be(payment.BillId);
		updatedPayment.CreatorId.Should().Be(newCreator);
		updatedPayment.IsCreatorIncluded.Should().Be(newIsCreatorIncluded);
		updatedPayment.Amount.Should().Be(newAmount);
		updatedPayment.DebtorIds.Should().BeEquivalentTo(newDebtors);

	}
}