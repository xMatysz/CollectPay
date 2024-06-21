using CollectPay.Application.Services;
using CollectPay.Domain.BillAggregate.Entities;
using CollectPay.Domain.BillAggregate.ValueObjects;
using CollectPay.Infrastructure.Services;

namespace CollectPay.Application.Tests.Unit.Services;

public class WhenUsingDebtService
{
	private readonly IDebtService _debtService = new DebtService();

	[Theory]
	[MemberData(nameof(CalculateData))]
	public void ShouldCalculateDebt(Payment[] payments, Debt[] expectedResults)
	{
		var result = _debtService.CalculateDebt(payments);

		result.Should().BeEquivalentTo(expectedResults);
	}

	public static IEnumerable<object[]> CalculateData()
	{
		yield return
		[
			Array.Empty<Payment>(),
			Array.Empty<Debt>()
		];

		var user1 = Guid.NewGuid();
		var user2 = Guid.NewGuid();
		var user3 = Guid.NewGuid();

		// do not return to himself
		yield return
		[
			new[]
			{
				PaymentBuilder.Default()
					.WithAmount(AmountBuilder.Default().WithAmount(100).WithCurrency("USD").Build())
					.WithCreatorId(user1)
					.WithDebtors([user1])
					.Build()
			},
			Array.Empty<Debt>()
		];

		// split equally int
		yield return
		[
			 new[]
			 {
				 PaymentBuilder.Default()
					 .WithAmount(AmountBuilder.Default().WithAmount(100).WithCurrency("USD").Build())
					 .WithCreatorId(user1)
					 .WithDebtors([user2, user3])
					 .Build(),
			 },
			new[]
			{
				Debt.Create(user2, 50, user1),
				Debt.Create(user3, 50, user1)
			}
		];

		// split equally float
		yield return
		[
			new[]
			{
				PaymentBuilder.Default()
					.WithAmount(AmountBuilder.Default().WithAmount(100).WithCurrency("USD").Build())
					.WithCreatorId(user1)
					.WithDebtors([user1, user2, user3])
					.Build(),

			},
			new[]
			{
				Debt.Create(user2, 33.33m, user1),
				Debt.Create(user3, 33.33m, user1)
			}
		];

		// debtor is user which never pay
		yield return
		[
			new[]
			{
				PaymentBuilder.Default()
					.WithAmount(AmountBuilder.Default().WithAmount(100).WithCurrency("USD").Build())
					.WithCreatorId(user1)
					.WithDebtors([user1, user2, user3])
					.Build(),

				PaymentBuilder.Default()
					.WithAmount(AmountBuilder.Default().WithAmount(100).WithCurrency("USD").Build())
					.WithCreatorId(user2)
					.WithDebtors([user1, user2, user3])
					.Build(),
			},
			new[]
			{
				Debt.Create(user3, 33.33m, user1),
				Debt.Create(user3, 33.33m, user2)
			}
		];

		//TODO: minimum transactions
	}
}