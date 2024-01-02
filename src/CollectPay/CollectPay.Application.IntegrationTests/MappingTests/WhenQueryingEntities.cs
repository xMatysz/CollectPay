namespace CollectPay.Application.IntegrationTests.MappingTests;

public class WhenQueryingEntities : IntegrationTestBase, IClassFixture<WebApiFactory>
{
	public WhenQueryingEntities(WebApiFactory factory)
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

		await BillRepository.AddAsync(bill);
		await UnitOfWork.SaveChangesAsync();

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

		await BillRepository.AddAsync(bill);
		await UnitOfWork.SaveChangesAsync();

		var allBills = await BillRepository.GetAllAsync();
		var billFromDb = allBills.Single();

		billFromDb.Payments.Should().HaveCount(2);
	}
}