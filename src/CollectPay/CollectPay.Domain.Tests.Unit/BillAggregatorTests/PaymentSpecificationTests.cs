using CollectPay.Domain.BillAggregate.Errors;

namespace CollectPay.Domain.Tests.Unit.BillAggregatorTests;

public class PaymentSpecificationTests
{
	[Theory]
	[InlineData(null)]
	[InlineData("")]
	[InlineData(" ")]
	public void Update_Fail_WhenNameIsEmpty(string? name)
	{
		var payment = PaymentBuilder.Default().Build();

		var result = payment.Update(
			name,
			PaymentBuilder.DefaultAmount,
			[],
			[]);

		result.IsError.Should().BeTrue();
		result.FirstError.Should().Be(PaymentErrors.NameCannotBeEmpty);
	}

	[Fact]
	public void Update_Fail_WhenAmountIsNull()
	{
		var payment = PaymentBuilder.Default().Build();

		var result = payment.Update(
			PaymentBuilder.DefaultName,
			null,
			[],
			[]);

		result.IsError.Should().BeTrue();
		result.FirstError.Should().Be(PaymentErrors.InvalidAmount);
	}

	[Fact]
	public void Update_Fail_WhenAnyUsersToAddAlreadyExist()
	{
		var existingUser = Guid.NewGuid();
		var payment = PaymentBuilder.Default().Build();
		payment.Update(
			PaymentBuilder.DefaultName,
			PaymentBuilder.DefaultAmount,
			[existingUser],
			[]);

		var result = payment.Update(
			PaymentBuilder.DefaultName,
			PaymentBuilder.DefaultAmount,
			[existingUser],
			[]);

		result.IsError.Should().BeTrue();
		result.FirstError.Should().Be(PaymentErrors.UserIsAlreadyAdded(existingUser));
	}

	[Fact]
	public void Update_Fail_WhenAnyUsersToRemoveNotExist()
	{
		var payment = PaymentBuilder.Default().Build();
		var notExistingUser = Guid.NewGuid();

		var result = payment.Update(
			PaymentBuilder.DefaultName,
			PaymentBuilder.DefaultAmount,
			[],
			[notExistingUser]);

		result.IsError.Should().BeTrue();
		result.FirstError.Should().Be(PaymentErrors.UserNotFound(notExistingUser));
	}
}