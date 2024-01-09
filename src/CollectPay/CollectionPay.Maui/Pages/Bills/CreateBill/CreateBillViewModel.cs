using System.ComponentModel.DataAnnotations;
using CollectionPay.Maui.Common;
using CollectionPay.Maui.Pages.Bills.BillList;
using CollectionPay.Maui.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace CollectionPay.Maui.Pages.Bills.CreateBill;

public partial class CreateBillViewModel : ViewModelBase
{
	private readonly IShellService _shellService;
	private readonly IBillService _billService;

	[ObservableProperty]
	[NotifyDataErrorInfo]
	[Required]
	private string _billName;

	[ObservableProperty]
	private bool _billNameHasError;

	partial void OnBillNameChanged(string value) => BillNameHasError = GetErrors(nameof(BillName)).Any();

	public CreateBillViewModel(IShellService shellService, IBillService billService)
	{
		Title = "Create bill";
		_shellService = shellService;
		_billService = billService;
	}

	[RelayCommand]
	private async Task CreateBill()
	{
		var bill = new BillModel(Guid.NewGuid(), BillName);

		await OnLoadingAsync(async () =>
		{
			await _billService.CreateBillAsync(bill);
		});

		await _shellService.GoToAsync("..");
	}
}