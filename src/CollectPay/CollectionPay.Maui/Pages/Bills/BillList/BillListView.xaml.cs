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
		base.OnAppearing();
		var vm = (BillListViewModel)BindingContext;
		vm.GetBillsCommand.ExecuteAsync(null);
	}
}