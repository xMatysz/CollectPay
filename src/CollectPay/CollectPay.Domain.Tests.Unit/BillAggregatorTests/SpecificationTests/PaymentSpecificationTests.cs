using CollectPay.Domain.BillAggregate.Entities;
using CollectPay.Domain.BillAggregate.Errors;

namespace CollectPay.Domain.Tests.Unit.BillAggregatorTests.SpecificationTests;

public class PaymentSpecificationTests
{
	[Fact]
	public void ShouldFailWhenCreatorIsInDebtorsList()
	{
		var user1 = Guid.NewGuid();
		var user2 = Guid.NewGuid();

		var payment = Payment
			.Create(Guid.NewGuid(),
				"Test",
				user1,
				false,
				null!,
				new[] { user1, user2 });

		payment.IsError.Should().BeTrue();
		payment.FirstError.Should().BeEquivalentTo(PaymentErrors.CreatorCannotBeDebtor);
	}
}