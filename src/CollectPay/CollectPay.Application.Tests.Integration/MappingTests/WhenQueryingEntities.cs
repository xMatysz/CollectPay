using CollectPay.Tests.Integration.Shared;

namespace CollectPay.Application.Tests.Integration.MappingTests;

public class WhenQueryingEntities : IntegrationTestBase
{
	public WhenQueryingEntities(WebApiFactory factory)
		: base(factory)
	{
	}

	[Fact]
	public async Task ShouldAttachPaymentsToBill()
	{
		var payments = new[] { new PaymentBuilder().Build(), new PaymentBuilder().Build() };
		var bill = new BillBuilder().Build();

		foreach (var payment in payments)
		{
			bill.AddPayment(payment);
		}

		await BillRepository.AddAsync(bill);
		await UnitOfWork.SaveChangesAsync();

		var billFromDb = await BillRepository.GetByIdAsync(bill.Id);

		billFromDb!.Payments.Should().HaveCount(2);
	}
}