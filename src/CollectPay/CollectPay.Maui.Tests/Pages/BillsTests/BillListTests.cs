using CollectionPay.Maui.Pages.Bills.BillList;
using CollectionPay.Maui.Services;

namespace CollectPay.Maui.Tests.Pages.BillsTests;

public class BillListTests
{
	private readonly BillListViewModel _sut;
	private readonly IBillService _billService;

	public BillListTests()
	{
		var connectivity = Substitute.For<IConnectivity>();
		connectivity.NetworkAccess.Returns(NetworkAccess.Internet);

		_billService = Substitute.For<IBillService>();
		_sut = new BillListViewModel(connectivity, _billService);
	}

	[Fact]
	public async Task ShouldLoadAllBills()
	{
		_billService.GetAllBillsAsync()
			.Returns(Task.FromResult(
				new[]
				{
					new BillModel(Guid.NewGuid(), "Test1"),
					new BillModel(Guid.NewGuid(), "Test2")
				}));

		await _sut.GetBillsCommand.ExecuteAsync(null);

		_sut.Bills.Should().HaveCount(2);
	}
}