using CollectPay.Application.BillAggregate.Queries.Bills.GetBills;
using CollectPay.Application.Common;

namespace CollectPay.Application.Tests.Unit.BillAggregateTests.Queries.Bills.GetBillsTests;

public class WhenValidatingGetBillsQuery
{
	private readonly GetBillsQueryInputValidator _validator = new();

	[Fact]
	public async Task Should_Fail_WhenUserIdIsEmpty()
	{
		var query = new GetBillsQuery(Guid.Empty);

		var result = await _validator.ValidateAsync(query);

		result.IsValid.Should().BeFalse();
		result.Errors.Single().ErrorMessage.Should()
			.Be(ValidationMessages.PropertyIsRequired(nameof(GetBillsQuery.UserId)));
	}
}