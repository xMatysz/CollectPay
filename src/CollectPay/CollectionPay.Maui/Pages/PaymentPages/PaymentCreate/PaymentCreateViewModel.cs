using CollectionPay.Contracts.Requests.Payment;
using CollectionPay.Contracts.Routes;
using CollectionPay.Maui.Abstraction;
using CollectionPay.Maui.Models;
using CollectionPay.Maui.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace CollectionPay.Maui.Pages.PaymentPages.PaymentCreate;

[QueryProperty(nameof(BillId), "modelId")]
public partial class PaymentCreateViewModel : ViewModelBase
{
	private readonly IShellService _shellService;
	private readonly IApiClient _apiClient;

	[ObservableProperty]
	private PaymentModel _payment = new();

	[ObservableProperty]
	private Guid _billId;

	public PaymentCreateViewModel(IShellService shellService, IApiClient apiClient)
	{
		_shellService = shellService;
		_apiClient = apiClient;
		Title = "Create Payment";
	}

	[RelayCommand]
	private void Clear()
	{
		Payment.Image = null;
		Payment.Name = null;
		Payment.Amount = 0m;
		Payment.Currency = null;
		OnPropertyChanged(nameof(Payment));
	}

	[RelayCommand]
	private async Task CreatePayment()
	{
		var request = new CreatePaymentRequest(Payment.Name, BillId, Payment.Amount, Payment.Currency, []);

		var response = await _apiClient.SendPost(PaymentRoutes.Create, request, CancellationToken.None);

		if (response.IsSuccessStatusCode)
		{
			await _shellService.GoToAsync("..");
			return;
		}

		await Shell.Current.DisplayAlert("Error", "Something wrong", "Ok");
	}
}