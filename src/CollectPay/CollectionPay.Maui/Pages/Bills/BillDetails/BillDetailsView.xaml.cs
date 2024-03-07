namespace CollectionPay.Maui.Pages.Bills.BillDetails;

public partial class BillDetailsView : ContentPage
{
	public BillDetailsView(BillDetailsViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}