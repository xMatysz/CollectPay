namespace CollectionPay.Maui.Pages.LoginPages.Register;

public partial class RegisterPage
{
	public RegisterPage(RegisterViewModel viewModel)
		: base(viewModel)
	{
		InitializeComponent();
	}

	protected override void OnAppearing()
	{
		ViewModel.ClearModel();
	}
}