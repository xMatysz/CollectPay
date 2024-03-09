namespace CollectionPay.Maui.Pages.Bills.BillDetails;

public partial class BillDetailsView : ContentPage
{
	public BillDetailsView(BillDetailsViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}

	protected override void OnAppearing()
	{
		base.OnAppearing();
		var vm = (BillDetailsViewModel)BindingContext;
		vm.GetPaymentsCommand.ExecuteAsync(vm.Bill);
	}
}