using System.Collections.ObjectModel;
using CollectionPay.Maui.Common;
using CollectionPay.Maui.Pages.Bills.CreateBill;
using CollectionPay.Maui.Services;
using CommunityToolkit.Mvvm.Input;

namespace CollectionPay.Maui.Pages.Bills.BillList;

public partial class BillListViewModel : ViewModelBase
{
	private readonly IShellService _shellService;
	private readonly IConnectivity _connectivity;
	private readonly IBillService _billService;

	public ObservableCollection<BillModel> Bills { get; } = new();

	public BillListViewModel(IShellService shellService, IConnectivity connectivity, IBillService billService)
	{
		_shellService = shellService;
		_connectivity = connectivity;
		_billService = billService;
	}

	[RelayCommand]
	public async Task GetBills()
	{
		if (_connectivity.NetworkAccess != NetworkAccess.Internet)
		{
			await _shellService.DisplayAlert("No internet connection",
				"Cannot access internet, try again later");
			return;
		}

		await OnRefreshingAsync(async () =>
		{
			BillModel[] bills;
			try
			{
				bills = await _billService.GetAllBillsAsync();
			}
			catch (Exception e)
			{
				await _shellService.ShowError(e.Message);
				return;
			}

			Bills.Clear();
			foreach (var bill in bills)
			{
				Bills.Add(bill);
			}
		});
	}

	[RelayCommand]
	public async Task GoToCreate()
	{
		await Shell.Current.GoToAsync(nameof(CreateBillView));
	}
}