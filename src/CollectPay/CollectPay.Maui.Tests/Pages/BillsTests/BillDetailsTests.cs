using CollectionPay.Maui.Pages.Bills.BillDetails;
using CollectionPay.Maui.Pages.Bills.BillList;
using CollectionPay.Maui.Services;

namespace CollectPay.Maui.Tests.Pages.BillsTests;

public class BillDetailsTests
{
	private readonly BillDetailsViewModel _sut;
	private readonly IBillService _billService;

	public BillDetailsTests()
	{
		var connectivity = Substitute.For<IConnectivity>();
		connectivity.NetworkAccess.Returns(NetworkAccess.Internet);

		_billService = Substitute.For<IBillService>();
		_sut = new BillDetailsViewModel(_billService);
	}

	[Fact]
	public async Task ShouldLoadAllBills()
	{
		var bill = new BillModel(Guid.NewGuid(), "TestName");
		_sut.Bill = bill;

		_billService.GetAllPaymentsForBill(Arg.Is(bill.Id))
			.Returns(Task.FromResult(
				new[]
				{
					new PaymentModel
					{
						CreatorId = Guid.NewGuid()
					},
					new PaymentModel
					{
						CreatorId = Guid.NewGuid()
					}
				}));

		await _sut.GetPaymentsCommand.ExecuteAsync(null);

		_sut.Payments.Should().HaveCount(2);
	}
}