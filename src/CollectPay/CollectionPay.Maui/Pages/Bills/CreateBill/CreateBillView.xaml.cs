namespace CollectionPay.Maui.Pages.Bills.CreateBill;

public partial class CreateBillView : ContentPage
{
	public CreateBillView(CreateBillViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}