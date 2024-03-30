using System.ComponentModel.DataAnnotations;

namespace CollectionPay.Maui.Pages.PaymentPages.PaymentCreate;

public partial class PaymentCreatePage
{
	public PaymentCreatePage(PaymentCreateViewModel viewModel)
		: base(viewModel)
	{
		InitializeComponent();
	}
}