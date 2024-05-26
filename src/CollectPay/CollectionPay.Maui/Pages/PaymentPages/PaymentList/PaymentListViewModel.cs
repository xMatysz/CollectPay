using System.Collections.ObjectModel;
using System.Net.Http.Json;
using CollectionPay.Contracts.Responses;
using CollectionPay.Contracts.Routes;
using CollectionPay.Maui.Abstraction;
using CollectionPay.Maui.Models;
using CollectionPay.Maui.Pages.BillPages.BillDetails;
using CollectionPay.Maui.Pages.PaymentPages.PaymentCreate;
using CollectionPay.Maui.Pages.PaymentPages.PaymentDetails;
using CollectionPay.Maui.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace CollectionPay.Maui.Pages.PaymentPages.PaymentList;

public partial class PaymentListViewModel : ViewModelBase, IHaveDataToLoad, IQueryAttributable
{
	private readonly IDispatcher _dispatcher;
	private readonly IShellService _shellService;
	private readonly IApiClient _apiClient;

	[ObservableProperty]
	private BillModel _bill;

	public bool IsDataLoaded => Payments.Any();

	public ObservableCollection<PaymentModel> Payments { get; } = new();

	public PaymentListViewModel(IDispatcher dispatcher, IShellService shellService, IApiClient apiClient)
	{
		_dispatcher = dispatcher;
		_shellService = shellService;
		_apiClient = apiClient;
	}

	public void ApplyQueryAttributes(IDictionary<string, object> query)
	{
		Bill = (BillModel)query["model"];
		Title = Bill.Name;
	}

	[RelayCommand]
	private async Task Refresh()
	{
		IsRefreshing = true;

		await LoadPayments();

		IsRefreshing = false;
	}


	[RelayCommand]
	private async Task GoToPaymentDetails(PaymentModel model)
	{
		await _shellService.GoToAsync(AppShell.GetRoute<PaymentDetailsPage>(), new Dictionary<string, object>
		{
			["model"] = model
		});
	}

	[RelayCommand]
	private async Task GoToPaymentCreate()
	{
		await _shellService.GoToAsync(AppShell.GetRoute<PaymentCreatePage>(), new Dictionary<string, object>
		{
			{"modelId", Bill.Id}
		});
	}

	[RelayCommand]
	private async Task GoToBillDetails()
	{
		await _shellService.GoToAsync(AppShell.GetRoute<BillDetailsPage>(), new Dictionary<string, object>()
		{
			{"model", Bill}
		});
	}

	private async Task LoadPayments()
	{
		IsBusy = true;

		await _dispatcher.DispatchAsync(Payments.Clear);

		var response = await _apiClient.SendGet($"{PaymentRoutes.List}?billId={Bill.Id}", CancellationToken.None);

		if (!response.IsSuccessStatusCode)
		{
			await Shell.Current.DisplayAlert("Error", "SomethingWrong", "Ok");
			return;
		}

		var payments = await response.Content.ReadFromJsonAsync<GetPaymentsResponse[]>(CancellationToken.None);

		foreach (var payment in payments)
		{
			var model = new PaymentModel(payment.Name, payment.Amount, payment.Currency);
			await _dispatcher.DispatchAsync(() => Payments.Add(model));
		}

		IsBusy = false;
	}
}