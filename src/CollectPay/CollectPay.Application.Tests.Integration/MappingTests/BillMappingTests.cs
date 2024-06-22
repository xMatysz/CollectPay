using CollectPay.Domain.BillAggregate;
using CollectPay.Domain.BillAggregate.Entities;
using CollectPay.Tests.Integration.Shared;
using Microsoft.EntityFrameworkCore;

namespace CollectPay.Application.Tests.Integration.MappingTests;

public class BillMappingTests : IntegrationTestBase
{
	public BillMappingTests(WebApiFactory factory)
		: base(factory)
	{
	}

	[Fact]
	public async Task Should_MapAllProperties()
	{
		var creator = Guid.NewGuid();
		var debtor1 = Guid.NewGuid();
		var debtor2 = Guid.NewGuid();
		const string billName = "VACation";
		Payment[] payments = [PaymentBuilder.Default().Build(), PaymentBuilder.Default().Build()];
		var bill = BillBuilder.Default()
			.WithCreatorId(creator)
			.WithName(billName)
			.WithPayments(payments)
			.WithDebtors([debtor1, debtor2])
			.Build();

		await AssumeEntityInDbAsync(bill);

		var billFromDb = await DbContext.Set<Bill>().FindAsync(bill.Id);

		billFromDb.Should().NotBeNull();
		billFromDb.Equals(bill).Should().BeTrue();
		billFromDb.Name.Should().Be(billFromDb.Name);
		billFromDb.CreatorId.Should().Be(billFromDb.CreatorId);
		billFromDb.Debtors.Should().HaveCount(3);
		billFromDb.Debtors.Should().BeEquivalentTo([creator, debtor1, debtor2]);
	}

	[Fact]
	public async Task Should_AttachPaymentsToBill()
	{
		var bill = BillBuilder.Default().Build();
		var payment = PaymentBuilder.Default().Build();
		bill.AddPayment(payment);
		var payment2 = PaymentBuilder.Default().Build();
		bill.AddPayment(payment2);
		await AssumeEntityInDbAsync(bill);

		var billFromDb = await DbContext
			.Set<Bill>()
			.Include(x => x.Payments)
			.FirstOrDefaultAsync(x => x.Id == bill.Id);

		billFromDb.Should().NotBeNull();
		billFromDb.Equals(bill).Should().BeTrue();
		billFromDb.Name.Should().Be(billFromDb.Name);
		billFromDb.CreatorId.Should().Be(billFromDb.CreatorId);
	}
}