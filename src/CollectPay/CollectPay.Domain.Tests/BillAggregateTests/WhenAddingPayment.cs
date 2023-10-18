using CollectPay.Domain.BillAggregate;
using CollectPay.Domain.Tests.TestsUtilities;

namespace CollectPay.Domain.Tests.BillAggregateTests;

public class WhenAddingPayment
{
	private readonly Bill _bill = new BillBuilder().Build();

	[Fact]
	public void ShouldAddToPayments()
	{
		var payment = new PaymentBuilder().Build();

		_bill.AddPayment(payment);

		_bill.Payments.Should().HaveCount(1);
	}

	[Fact]
	public void ShouldGenerateDebts()
	{
		var creatorId = Guid.NewGuid();
		var debtorId = Guid.NewGuid();
		const decimal debtAmount = 10m;
		var payment = new PaymentBuilder()
			.WithCreatorId(creatorId)
			.WithAmount(debtAmount)
			.WithDebtors(new List<Guid> { debtorId })
			.Build();

		_bill.AddPayment(payment);

		_bill.Payments.Should().HaveCount(1);
		var debts = _bill.CalculateDebts();
		debts.Should().HaveCount(1);
		var debt = debts.First();
		debt.Creditor.Should().Be(creatorId);
		debt.Debtor.Should().Be(debtorId);
		debt.DebtAmount.Should().Be(debtAmount);
	}
}