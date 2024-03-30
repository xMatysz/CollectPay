using CollectionPay.Maui.Abstraction;
using CollectionPay.Maui.Models;
using CommunityToolkit.Mvvm.ComponentModel;

namespace CollectionPay.Maui.Pages.PaymentPages.PaymentDetails;

public partial class PaymentDetailsViewModel : ViewModelBase
{

	[ObservableProperty]
	private PaymentCreateModel _model = new();
}