namespace CollectionPay.Maui.Pages.LoginPages.Login;

public partial class LoginPage
{
	public LoginPage(LoginViewModel viewModel)
		: base(viewModel)
	{
		InitializeComponent();
	}

	protected override void OnAppearing()
	{
		ViewModel.ClearModel();
	}
}