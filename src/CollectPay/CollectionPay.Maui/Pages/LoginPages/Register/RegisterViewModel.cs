using CollectionPay.Maui.Abstraction;
using CollectionPay.Maui.Models;
using CollectionPay.Maui.Pages.LoginPages.Login;
using CollectionPay.Maui.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace CollectionPay.Maui.Pages.LoginPages.Register;

public partial class RegisterViewModel : ViewModelBase
{
	private readonly ILoginService _loginService;
	private readonly IShellService _shellService;

	[ObservableProperty]
	private RegisterModel _model = new();

	public RegisterViewModel(ILoginService loginService, IShellService shellService)
	{
		_loginService = loginService;
		_shellService = shellService;
	}

	[RelayCommand]
	private async Task Register()
	{
		if (Model.Password != Model.ConfirmPassword)
		{
			await Shell.Current.DisplayAlert("Error", "Incorrect Passwords", "Ok");
		}
		else
		{
			await _loginService.RegisterAsync(Model);
			await _shellService.GoToAsync("..");
		}
	}

	[RelayCommand]
	public async Task GoToLoginPage()
	{
		await Shell.Current.GoToAsync(AppShell.GetRoute<LoginPage>());
	}
}