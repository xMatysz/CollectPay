using CollectionPay.Maui.Abstraction;
using CollectionPay.Maui.Models;
using CommunityToolkit.Mvvm.ComponentModel;

namespace CollectionPay.Maui.Pages.PaymentPages.PaymentCreate;

[QueryProperty(nameof(Payment), "model")]
public partial class PaymentCreateViewModel : ViewModelBase
{

	[ObservableProperty]
	private PaymentModel _payment;
}