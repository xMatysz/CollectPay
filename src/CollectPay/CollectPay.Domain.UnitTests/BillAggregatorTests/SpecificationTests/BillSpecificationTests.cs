using CollectPay.Domain.BillAggregate;
using CollectPay.Domain.BillAggregate.Errors;

namespace CollectPay.Domain.UnitTests.BillAggregatorTests.SpecificationTests;

public class BillSpecificationTests
{
	private readonly Bill _bill = new BillBuilder().Build();

	[Fact]
	public void ShouldAddPayment()
	{
		var payment = new PaymentBuilder().Build();

		_bill.AddPayment(payment);

		_bill.Payments.Should().HaveCount(1);
		_bill.Payments.Should().BeEquivalentTo(new[] { payment });
	}

	[Fact]
	public void ShouldDeletePayment()
	{
		var payment = new PaymentBuilder().Build();
		_bill.AddPayment(payment);

		_bill.RemovePayment(payment.Id);

		_bill.Payments.Should().BeEmpty();
	}

	[Fact]
	public void ShouldFailWhenAddingInvalidPayment()
	{
		var result = _bill.AddPayment(null);

		result.IsError.Should().BeTrue();
		result.FirstError.Should().BeEquivalentTo(PaymentErrors.InvalidPayment);
	}

	[Fact]
	public void ShouldFailWhenAddingAlreadyExistingPayment()
	{
		var payment = new PaymentBuilder().Build();
		_bill.AddPayment(payment);

		var result = _bill.AddPayment(payment);

		result.IsError.Should().BeTrue();
		result.FirstError.Should().BeEquivalentTo(PaymentErrors.PaymentAlreadyExist);
	}

	[Fact]
	public void ShouldFailWhenPaymentToDeleteNotExist()
	{
		var payment = new PaymentBuilder().Build();

		var result = _bill.RemovePayment(payment.Id);

		result.IsError.Should().BeTrue();
		result.FirstError.Should().BeEquivalentTo(PaymentErrors.PaymentNotFound);
	}
}