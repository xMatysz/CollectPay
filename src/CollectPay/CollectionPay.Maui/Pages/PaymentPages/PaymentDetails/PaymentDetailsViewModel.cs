using CollectionPay.Maui.Abstraction;
using CollectionPay.Maui.Models;
using CommunityToolkit.Mvvm.ComponentModel;

namespace CollectionPay.Maui.Pages.PaymentPages.PaymentDetails;

public partial class PaymentDetailsViewModel : ViewModelBase, IQueryAttributable
{
	[ObservableProperty]
	private PaymentModel _model = new();

	public void ApplyQueryAttributes(IDictionary<string, object> query)
	{
		var item = query["model"] as PaymentModel ?? throw new InvalidCastException();
		Model = item;
		Title = $"Edit {Model.Name}";
	}
}