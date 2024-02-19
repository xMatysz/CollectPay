using CollectPay.Application.Services;
using CollectPay.Domain.BillAggregate.Entities;
using CollectPay.Domain.BillAggregate.ValueObjects;
using CollectPay.Infrastructure.Services;
using CollectPay.Tests.Shared.Builders;
using FluentAssertions;
using Xunit;

namespace CollectPay.Infrastructure.UnitTests.ServicesTests;

public class WhenUsingDebtService
{
	private readonly IDebtService _debtService = new DebtService();

	[Theory]
	[MemberData(nameof(CalculateData))]
	public async Task ShouldCalculateDebt(List<Payment> payments, List<Debt> expectedResults)
	{
		var result = await _debtService.CalculateDebt(payments);

		result.Should().BeEquivalentTo(expectedResults);
	}

	public static IEnumerable<object[]> CalculateData()
	{
		var pb = new PaymentBuilder();
		var user1 = Guid.NewGuid();
		var user2 = Guid.NewGuid();
		var user3 = Guid.NewGuid();

		yield return
		[
			new List<Payment>
			{
				pb.WithCreatorId(user1)
					.WithAmount(Amount.Create(10m, "USD"))
					.WithDebtors(user2, user3)
					.Build()
			},
			new List<Debt>
			{
				Debt.Create(user2, 5m, user1),
				Debt.Create(user3, 5m, user1)
			}
		];

		yield return
		[
			new List<Payment>
			{
				pb.WithCreatorId(user1)
					.WithAmount(Amount.Create(10m, "USD"))
					.WithDebtors(user2, user3)
					.WithCreatorIncluded()
					.Build(),
			},
			new List<Debt>
			{
				Debt.Create(user2, 3.33m, user1),
				Debt.Create(user3, 3.33m, user1)
			}
		];
	}
}
