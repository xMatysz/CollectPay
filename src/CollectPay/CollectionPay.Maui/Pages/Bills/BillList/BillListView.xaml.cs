namespace CollectionPay.Maui.Pages.Bills.BillList;

public partial class BillListView : ContentPage
{
	public BillListView(BillListViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}

	protected override void OnAppearing()
	{
		var vm = BindingContext as BillListViewModel;
		vm!.GetBillsCommand.ExecuteAsync(null);
	}
}