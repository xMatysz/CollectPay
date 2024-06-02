using CollectionPay.Maui.Abstraction;
using CollectionPay.Maui.Pages.LoginPages.Login;
using CollectionPay.Maui.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace CollectionPay.Maui.Pages.LoginPages.User;

public partial class UserViewModel : ViewModelBase
{
	private readonly IPreferences _preferences;
	private readonly ILoginService _loginService;

	[ObservableProperty]
	private string _email = "Hey@ema.com";

	public UserViewModel(
		IPreferences preferences,
		ILoginService loginService)
	{
		_preferences = preferences;
		_loginService = loginService;
	}

	[RelayCommand]
	public async Task Logout()
	{
		_loginService.LogOut();

		await Shell.Current.GoToAsync(AppShell.GetRoute<LoginPage>());
	}

	public void LoadUserData()
	{
		var email = _preferences.Get("email", "error");
		Email = email;
	}
}