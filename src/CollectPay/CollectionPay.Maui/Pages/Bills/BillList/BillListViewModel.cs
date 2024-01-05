using System.Collections.ObjectModel;
using CollectionPay.Maui.Common;
using CollectionPay.Maui.Pages.Bills.CreateBill;
using CollectionPay.Maui.Services;
using CommunityToolkit.Mvvm.Input;

namespace CollectionPay.Maui.Pages.Bills.BillList;

public partial class BillListViewModel : ViewModelBase
{
	private readonly IConnectivity _connectivity;
	private readonly IBillService _billService;

	public ObservableCollection<BillModel> Bills { get; } = new();

	public BillListViewModel(IConnectivity connectivity, IBillService billService)
	{
		_connectivity = connectivity;
		_billService = billService;
	}

	[RelayCommand]
	public async Task GetBills()
	{
		if (_connectivity.NetworkAccess != NetworkAccess.Internet)
		{
			await DisplayAlert("No internet connection",
				"Cannot access internet, try again later");
			return;
		}

		var bills = await _billService.GetAllBillsAsync();

		Bills.Clear();
		foreach (var bill in bills)
		{
			Bills.Add(bill);
		}

		IsRefreshing = false;
	}

	[RelayCommand]
	public async Task GoToCreate()
	{
		await Shell.Current.GoToAsync(nameof(CreateBillView));
	}
}