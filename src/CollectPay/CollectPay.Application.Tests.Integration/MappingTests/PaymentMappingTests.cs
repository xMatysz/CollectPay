using CollectPay.Domain.BillAggregate.Entities;
using CollectPay.Tests.Integration.Shared;
using Microsoft.EntityFrameworkCore;

namespace CollectPay.Application.Tests.Integration.MappingTests;

public class PaymentMappingTests : IntegrationTestBase
{
	public PaymentMappingTests(WebApiFactory factory)
		: base(factory)
	{
	}

	[Fact]
	public async Task Should_MapAllProperties()
	{
		var id = Guid.NewGuid();
		var creatorId = Guid.NewGuid();
		const string paymentName = "Drinks";
		var amount = AmountBuilder.Default().WithAmount(2137m).WithCurrency("EUR").Build();
		var debtor1 = Guid.NewGuid();
		var debtor2 = Guid.NewGuid();

		var bill = BillBuilder.Default().Build();
		var payment = PaymentBuilder.Default()
			.WithId(id)
			.WithCreatorId(creatorId)
			.WithName(paymentName)
			.WithAmount(amount)
			.WithDebtors([debtor1, debtor2])
			.Build();

		bill.AddPayment(payment);
		await AssumeEntityInDbAsync(bill);

		var paymentFromDb = await DbContext.Set<Payment>().SingleAsync();

		paymentFromDb.Should().NotBeNull();
		paymentFromDb.Id.Should().Be(id);
		paymentFromDb.CreatorId.Should().Be(creatorId);
		paymentFromDb.Name.Should().Be(paymentName);
		paymentFromDb.Debtors.Should().BeEquivalentTo([debtor1, debtor2]);
		paymentFromDb.Amount.Value.Should().Be(amount.Value);
		paymentFromDb.Amount.Currency.Should().Be(amount.Currency);
	}
}