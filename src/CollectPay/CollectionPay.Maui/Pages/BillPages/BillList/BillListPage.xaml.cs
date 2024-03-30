namespace CollectionPay.Maui.Pages.BillPages.BillList;

public partial class BillListPage
{
	public BillListPage(BillListViewModel viewModel)
		: base(viewModel)
	{
		InitializeComponent();
	}

	protected override void OnAppearing()
	{
		base.OnAppearing();

		var vm = (BillListViewModel)BindingContext;
		if (!vm.IsDataLoaded)
		{
			vm.IsRefreshing = true;
		}
	}
}