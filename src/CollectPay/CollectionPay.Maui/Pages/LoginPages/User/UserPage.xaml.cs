namespace CollectionPay.Maui.Pages.LoginPages.User;

public partial class UserPage
{
	public UserPage(UserViewModel vm)
		: base(vm)
	{
		InitializeComponent();
	}

	protected override void OnAppearing()
	{
		base.OnAppearing();

		ViewModel.LoadUserData();
	}
}