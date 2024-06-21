using CollectPay.Application.BillAggregate.Queries.Bills.GetDebts;
using CollectPay.Application.BillAggregate.Queries.Payments.GetPayments;
using CollectPay.Application.Common;

namespace CollectPay.Application.Tests.Unit.BillAggregateTests.Queries.Payments.GetPaymentsTests;

public class WhenValidatingGetPaymentsQuery
{
	private readonly GetPaymentsQueryInputValidator _validator = new();

	[Fact]
	public async Task Should_Fail_WhenUserIdEmpty()
	{
		var query = new GetPaymentsQuery(Guid.Empty, Guid.NewGuid());

		var result = await _validator.ValidateAsync(query);

		result.IsValid.Should().BeFalse();
		result.Errors.Single().ErrorMessage.Should()
			.Be(ValidationMessages.PropertyIsRequired(nameof(GetDebtsQuery.UserId)));
	}

	[Fact]
	public async Task Should_Fail_WhenBillIdEmpty()
	{
		var query = new GetPaymentsQuery(Guid.NewGuid(), Guid.Empty);

		var result = await _validator.ValidateAsync(query);

		result.IsValid.Should().BeFalse();
		result.Errors.Single().ErrorMessage.Should()
			.Be(ValidationMessages.PropertyIsRequired(nameof(GetDebtsQuery.BillId)));
	}
}