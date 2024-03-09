using CollectionPay.Maui.Common;
using CollectionPay.Maui.Pages.Bills.BillList;
using CollectionPay.Maui.Services;
using NSubstitute.ReceivedExtensions;

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
		var shellService = Substitute.For<IShellService>();
		_sut = new BillListViewModel(shellService, connectivity, _billService);
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

	[Fact]
	public async Task ShouldSendDeleteBill()
	{
		_billService.DeleteBillAsync(Arg.Any<Guid>()).Returns(Task.CompletedTask);
		var model = new BillModel(Guid.NewGuid(), "TestName");

		await _sut.DeleteBill(model);

		await _billService.Received(1).DeleteBillAsync(Arg.Is(model.Id));
	}
}