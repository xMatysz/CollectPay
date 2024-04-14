using System.Collections.ObjectModel;
using CollectionPay.Maui.Abstraction;
using CollectionPay.Maui.Models;
using CollectionPay.Maui.Pages.BillPages.BillCreate;
using CollectionPay.Maui.Pages.LoginPages.Login;
using CollectionPay.Maui.Pages.PaymentPages.PaymentList;
using CollectionPay.Maui.Services;
using CommunityToolkit.Mvvm.Input;

namespace CollectionPay.Maui.Pages.BillPages.BillList;

public sealed partial class BillListViewModel : ViewModelBase, IHaveDataToLoad
{
	private readonly IDispatcher _dispatcher;
	private readonly IShellService _shellService;

	public bool IsDataLoaded => Bills.Any();

	public ObservableCollection<BillModel> Bills { get; } = [];

	public BillListViewModel(IDispatcher dispatcher, IShellService shellService)
	{
 		_dispatcher = dispatcher;
		_shellService = shellService;

		Title = "Bill List";
	}

	[RelayCommand]
	private async Task Refresh()
	{
		IsRefreshing = true;

		await Task.Delay(600);
		await LoadBills();

		IsRefreshing = false;
	}

	[RelayCommand]
	private async Task RemoveBill(BillModel model)
	{
		IsBusy = true;

		await Task.Delay(1000);
		Bills.Remove(model);

		IsBusy = false;
	}

	[RelayCommand]
	private async Task GoToBillCreatePage()
	{
		// TEMP
		// await _shellService.GoToAsync(AppShell.GetRoute<BillCreatePage>());

		await _shellService.GoToAsync(AppShell.GetRoute<LoginPage>());
	}

	[RelayCommand]
	private async Task GoToBillDetailsPage(BillModel model)
	{
		await _shellService.GoToAsync(AppShell.GetRoute<PaymentListPage>(), new Dictionary<string, object>
		{
			["model"] = model
		});
	}

	private async Task LoadBills()
	{
		IsBusy = true;
		await _dispatcher.DispatchAsync(Bills.Clear).ConfigureAwait(false);

		var bills = new[]
		{
			new BillModel("Wakacje"),
			new BillModel("Tailandia"),
			new BillModel("Opłacenie rachunków za życzie"),
			new BillModel("Studniówka"),
		};

		foreach (var bill in bills)
		{
			await _dispatcher.DispatchAsync(() => Bills.Add(bill)).ConfigureAwait(false);
		}

		IsBusy = false;
	}
}