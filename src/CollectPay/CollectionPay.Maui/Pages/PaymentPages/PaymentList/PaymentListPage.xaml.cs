namespace CollectionPay.Maui.Pages.PaymentPages.PaymentList;

public partial class PaymentListPage
{
	public PaymentListPage(PaymentListViewModel viewModel)
		: base(viewModel)
	{
		InitializeComponent();
	}


	protected override void OnAppearing()
	{
		base.OnAppearing();

		var vm = (PaymentListViewModel)BindingContext;
		if (!vm.IsDataLoaded)
		{
			vm.IsRefreshing = true;
		}
	}
}