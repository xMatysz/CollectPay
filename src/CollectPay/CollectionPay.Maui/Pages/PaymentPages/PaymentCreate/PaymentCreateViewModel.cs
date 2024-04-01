using CollectionPay.Maui.Abstraction;
using CollectionPay.Maui.Models;
using CollectionPay.Maui.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace CollectionPay.Maui.Pages.PaymentPages.PaymentCreate;

public partial class PaymentCreateViewModel : ViewModelBase
{
	private readonly IShellService _shellService;

	[ObservableProperty]
	private PaymentModel _payment = new();

	public PaymentCreateViewModel(IShellService shellService)
	{
		_shellService = shellService;
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
		await _shellService.GoToAsync("..");
	}
}