using CollectPay.Domain.BillAggregate;
using CollectPay.Domain.Tests.TestsUtilities;

namespace CollectPay.Domain.Tests.BillAggregateTests;

public class WhenEditingPayments
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
	public void ShouldDeletePayment()
	{
		var payments = new[]
		{
			new PaymentBuilder().Build(),
			new PaymentBuilder().Build(),
			new PaymentBuilder().Build()
		};

		foreach (var payment in payments)
		{
			_bill.AddPayment(payment);
		}
		var paymentToDelete = payments.First();

		_bill.DeletePayment(paymentToDelete.Id);

		_bill.Payments.Should().HaveCount(2);
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