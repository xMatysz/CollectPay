using System.Collections.ObjectModel;
using System.Net.Http.Json;
using CollectionPay.Contracts.Responses;
using CollectionPay.Contracts.Routes;
using CollectionPay.Maui.Abstraction;
using CollectionPay.Maui.Models;
using CollectionPay.Maui.Pages.BillPages.BillCreate;
using CollectionPay.Maui.Pages.PaymentPages.PaymentList;
using CollectionPay.Maui.Services;
using CommunityToolkit.Mvvm.Input;

namespace CollectionPay.Maui.Pages.BillPages.BillList;

public sealed partial class BillListViewModel : ViewModelBase, IHaveDataToLoad
{
	private readonly IDispatcher _dispatcher;
	private readonly IShellService _shellService;
	private readonly IApiClient _apiClient;

	public bool IsDataLoaded => Bills.Any();

	public ObservableCollection<BillModel> Bills { get; } = [];

	public BillListViewModel(IDispatcher dispatcher, IShellService shellService, IApiClient apiClient)
	{
 		_dispatcher = dispatcher;
		_shellService = shellService;
		_apiClient = apiClient;

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
		await _shellService.GoToAsync(AppShell.GetRoute<BillCreatePage>());
	}

	[RelayCommand]
	private async Task GoToBillDetailsPage(BillModel model)
	{
		await _shellService.GoToAsync(AppShell.GetRoute<PaymentListPage>(), new Dictionary<string, object>
		{
			["model"] = model
		});
	}

	private async Task LoadBills(CancellationToken cancellationToken = default)
	{
		IsBusy = true;

		var response = await _apiClient.SendGet(BillRoutes.List, cancellationToken);

		var bills = await response.Content.ReadFromJsonAsync<GetBillsResponse[]>(cancellationToken);

		await _dispatcher.DispatchAsync(Bills.Clear).ConfigureAwait(false);

		foreach (var bill in bills)
		{
			var model = new BillModel(bill.Id, bill.Name, bill.Debtors);

			await _dispatcher.DispatchAsync(() => Bills.Add(model)).ConfigureAwait(false);
		}

		IsBusy = false;
	}
}