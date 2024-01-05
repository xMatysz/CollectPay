using System.ComponentModel.DataAnnotations;
using CollectionPay.Maui.Common;
using CollectionPay.Maui.Pages.Bills.BillList;
using CollectionPay.Maui.Services;
using CommunityToolkit.Mvvm.Input;

namespace CollectionPay.Maui.Pages.Bills.CreateBill;

public partial class CreateBillViewModel : ViewModelBase
{
	private readonly IBillService _billService;

	[Required]
	public string BillName { get; }

	public CreateBillViewModel(IBillService billService)
	{
		Title = "Create bill";
		_billService = billService;
	}

	[RelayCommand]
	private async Task CreateBill()
	{
		var bill = new BillModel(Guid.NewGuid(), BillName);
		await _billService.CreateBillAsync(bill);
	}
}