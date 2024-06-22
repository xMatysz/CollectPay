using CollectPay.Domain.BillAggregate;
using CollectPay.Domain.BillAggregate.Errors;

namespace CollectPay.Domain.Tests.Unit.BillAggregatorTests;

public class BillSpecificationTests
{
	private readonly Bill _bill = BillBuilder
		.Default()
		.Build();

	[Fact]
	public void AddPayments_Fail_WhenPaymentAlreadyExist()
	{
		var payment = PaymentBuilder
			.Default()
			.WithBillId(_bill.Id)
			.Build();
		_bill.AddPayment(payment);

		var result = _bill.AddPayment(payment);

		result.IsError.Should().BeTrue();
		result.FirstError.Should().Be(BillErrors.PaymentAlreadyExist);
	}

	[Fact]
	public void AddPayments_Should_AddPayments()
	{
		var payment = PaymentBuilder
			.Default()
			.WithBillId(_bill.Id)
			.Build();
		_bill.AddPayment(payment);

		var paymentWithDifferentId = PaymentBuilder
			.Like(payment)
			.WithId(Guid.NewGuid())
			.Build();

		var result = _bill.AddPayment(paymentWithDifferentId);

		result.IsError.Should().BeFalse();
		result.Value.Should().Be(Result.Updated);
	}

	[Fact]
	public void RemovePayments_Fail_WhenPaymentNotExist()
	{
		var notExistingPaymentId = Guid.NewGuid();

		var result = _bill.RemovePayment(notExistingPaymentId);

		result.IsError.Should().BeTrue();
		result.FirstError.Should().Be(BillErrors.PaymentNotExist);
	}

	[Fact]
	public void RemovePayments_Success_WhenPaymentExist()
	{
		var payment = PaymentBuilder
			.Default()
			.Build();
		_bill.AddPayment(payment);

		var result = _bill.RemovePayment(payment.Id);

		result.IsError.Should().BeFalse();
		result.Value.Should().Be(Result.Deleted);
	}

	[Theory]
	[InlineData("")]
	[InlineData(" ")]
	[InlineData(null)]
	public void Update_Fail_WhenNameIsEmpty(string? name)
	{
		var result = _bill.Update(name, [], []);

		result.IsError.Should().BeTrue();
		result.FirstError.Should().Be(BillErrors.NameCannotBeEmpty);
	}

	[Fact]
	public void Update_Fail_WhenAnyUsersToAddAlreadyExist()
	{
		var existingUser = Guid.NewGuid();
		_bill.Update(BillBuilder.DefaultName, [existingUser], []);

		var result = _bill.Update(BillBuilder.DefaultName, [existingUser], []);

		result.IsError.Should().BeTrue();
		result.FirstError.Should().Be(BillErrors.DebtorIsAlreadyAdded(existingUser));
	}

	[Fact]
	public void Update_Fail_WhenAnyUsersToRemoveNotExist()
	{
		var notExistingUser = Guid.NewGuid();
		var result = _bill.Update(BillBuilder.DefaultName, [], [notExistingUser]);

		result.IsError.Should().BeTrue();
		result.FirstError.Should().Be(BillErrors.DebtorNotFound(notExistingUser));
	}

	[Fact]
	public void Update_Fail_WhenUsersToRemoveContainsCreator()
	{
		var creatorId = Guid.NewGuid();
		var customBill = BillBuilder.Default().WithCreatorId(creatorId).Build();
		var result = customBill.Update(BillBuilder.DefaultName, [], [creatorId]);

		result.IsError.Should().BeTrue();
		result.FirstError.Should().Be(BillErrors.CannotRemoveCreatorFromDebtors);
	}

	[Fact]
	public void Update_Should_UpdateName()
	{
		const string newName = "Vacation";

		_bill.Update(newName, [], []);

		_bill.Name.Should().Be(newName);
	}

	[Fact]
	public void Update_Should_AddDebtors()
	{
		var newDebtor = Guid.NewGuid();

		_bill.Update(_bill.Name, [newDebtor], []);

		_bill.Debtors.Should().Contain(newDebtor);
	}

	[Fact]
	public void Update_Should_RemoveDebtors()
	{
		var existingDebtor = Guid.NewGuid();
		_bill.Update(_bill.Name, [existingDebtor], []);
		_bill.Debtors.Should().Contain(existingDebtor);

		_bill.Update(_bill.Name, [], [existingDebtor]);

		_bill.Debtors.Should().NotContain(existingDebtor);
	}
}