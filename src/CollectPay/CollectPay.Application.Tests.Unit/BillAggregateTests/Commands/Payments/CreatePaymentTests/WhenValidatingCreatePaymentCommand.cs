using CollectPay.Application.BillAggregate.Commands.Payments.CreatePayment;
using CollectPay.Application.Common;
using CollectPay.Domain.BillAggregate.ValueObjects;

namespace CollectPay.Application.Tests.Unit.BillAggregateTests.Commands.Payments.CreatePaymentTests;

public class WhenValidatingCreatePaymentCommand
{
	private readonly CreatePaymentCommandValidator _validator = new();

	[Fact]
	public async Task Should_Fail_WhenNameIsEmpty()
	{
		var command = GetCommand(name: string.Empty);

		var result = await _validator.ValidateAsync(command);

		result.IsValid.Should().BeFalse();
		result.Errors.Single().ErrorMessage.Should()
			.Be(ValidationMessages.PropertyIsRequired(nameof(CreatePaymentCommand.Name)));
	}

	[Fact]
	public async Task Should_Fail_WhenBillIdIsEmpty()
	{
		var command = GetCommand(billId: Guid.Empty);

		var result = await _validator.ValidateAsync(command);

		result.IsValid.Should().BeFalse();
		result.Errors.Single().ErrorMessage.Should()
			.Be(ValidationMessages.PropertyIsRequired(nameof(CreatePaymentCommand.BillId)));
	}

	[Fact]
	public async Task Should_Fail_WhenCreatorIdIsEmpty()
	{
		var command = GetCommand(creatorId: Guid.Empty);

		var result = await _validator.ValidateAsync(command);

		result.IsValid.Should().BeFalse();
		result.Errors.Single().ErrorMessage.Should()
			.Be(ValidationMessages.PropertyIsRequired(nameof(CreatePaymentCommand.CreatorId)));
	}

	[Fact]
	public async Task Should_Fail_WhenAmountIsNull()
	{
		var command = new CreatePaymentCommand(
			"TestName",
			Guid.NewGuid(),
			Guid.NewGuid(),
			null,
			[Guid.NewGuid()]);

		var result = await _validator.ValidateAsync(command);

		result.IsValid.Should().BeFalse();
		result.Errors.Single().ErrorMessage.Should()
			.Be(ValidationMessages.PropertyIsRequired(nameof(CreatePaymentCommand.Amount)));
	}

	[Fact]
	public async Task Should_Fail_WhenDebtorsAreEmpty()
	{
		var command = GetCommand(debtors: []);

		var result = await _validator.ValidateAsync(command);

		result.IsValid.Should().BeFalse();
		result.Errors.Single().ErrorMessage.Should()
			.Be(ValidationMessages.PropertyIsRequired(nameof(CreatePaymentCommand.Debtors)));
	}

	private static CreatePaymentCommand GetCommand(
		string? name = null,
		Guid? billId = null,
		Guid? creatorId = null,
		Amount? amount = null,
		Guid[]? debtors = null)
	{
		return new CreatePaymentCommand(
			name ?? "TestName",
			billId ?? Guid.NewGuid(),
			creatorId ?? Guid.NewGuid(),
			amount ?? AmountBuilder.Default().Build(),
			debtors ?? [Guid.NewGuid()]);

	}
}