namespace CollectionPay.Maui.Pages.Bills.BillList;

public partial class BillListView : ContentPage
{
	public BillListView(BillListViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}