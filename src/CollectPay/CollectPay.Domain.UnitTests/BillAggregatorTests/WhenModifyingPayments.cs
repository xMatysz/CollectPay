using CollectPay.Domain.BillAggregate;
using CollectPay.Domain.BillAggregate.Errors;
using ErrorOr;

namespace CollectPay.Domain.UnitTests.BillAggregatorTests;

public class WhenModifyingPayments
{
	private readonly Bill _bill = new BillBuilder().Build();

	[Fact]
	public void ShouldFailWhenPaymentNotExist()
	{
		var paymentToAdd = new PaymentBuilder().Build();
		_bill.AddPayment(paymentToAdd);
		var notAddedPayment = new PaymentBuilder().Build();

		var result =  _bill.RemovePayment(notAddedPayment.Id);

		result.IsError.Should().BeTrue();
		result.FirstError.Should().BeEquivalentTo(PaymentErrors.PaymentNotFound);
		_bill.Payments.Should().HaveCount(1);
	}

	[Fact]
	public void ShouldFailWhenAddingExistPayment()
	{
		var bill = new BillBuilder().Build();
		var payment = new PaymentBuilder().Build();
		bill.AddPayment(payment);

		var result =  bill.AddPayment(payment);

		result.IsError.Should().BeTrue();
		result.FirstError.Should().BeEquivalentTo(PaymentErrors.PaymentAlreadyExist);
		bill.Payments.Should().HaveCount(1);
	}
}