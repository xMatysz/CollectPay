namespace CollectPay.Application.IntegrationTests.MappingTests;

public class WhenQueryingBillEntities : IntegrationTestBase, IClassFixture<WebApiFactory>
{
	public WhenQueryingBillEntities(WebApiFactory factory)
		: base(factory)
	{
	}

	[Fact]
	public async Task ShouldGetCorrectBillFromDb()
	{
		const string billName = "vacation";
		var creatorId = Guid.NewGuid();
		var bill = BillBuilder
			.WithName(billName)
			.WithCreatorId(creatorId)
			.Build();

		await AddEntityToDbAsync(bill);

		var allBills = await BillRepository.GetAllAsync();
		var billFromDb = allBills.Single();

		billFromDb.Name.Should().Be(billName);
		billFromDb.CreatorId.Should().Be(creatorId);
	}

	[Fact]
	public async Task ShouldAttachPaymentsToBill()
	{
		var payments = new[] { PaymentBuilder.Build(), PaymentBuilder.Build() };
		var bill = BillBuilder.Build();

		foreach (var payment in payments)
		{
			bill.AddPayment(payment);
		}

		await AddEntityToDbAsync(bill);

		var allBills = await BillRepository.GetAllAsync();
		var billFromDb = allBills.Single();

		billFromDb.Payments.Should().HaveCount(2);
	}
}