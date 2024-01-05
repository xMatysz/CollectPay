﻿using CollectionPay.Maui.Pages.Bills.BillList;
using CollectionPay.Maui.Pages.Bills.CreateBill;
using CollectionPay.Maui.Services;

namespace CollectPay.Maui.Tests.Pages.BillsTests;

public class CreateBillTests
{
	private readonly CreateBillViewModel _sut;
	private readonly IBillService _billService;

	public CreateBillTests()
	{
		_billService = Substitute.For<IBillService>();
		_sut = new CreateBillViewModel(_billService);
	}

	[Fact]
	public async Task ShouldSendCreateBillRequest()
	{
		await _sut.CreateBillCommand.ExecuteAsync(null);

		await _billService.Received(1).CreateBillAsync(Arg.Any<BillModel>(), Arg.Any<CancellationToken>());
	}
}