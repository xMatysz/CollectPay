using CollectionPay.Maui.Abstraction;
using CollectionPay.Maui.Models;
using CollectionPay.Maui.Pages.BillPages.BillList;
using CollectionPay.Maui.Pages.LoginPages.Register;
using CollectionPay.Maui.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace CollectionPay.Maui.Pages.LoginPages.Login;

public partial class LoginViewModel : ViewModelBase
{
	private readonly IShellService _shellService;
	private readonly ILoginService _loginService;

	[ObservableProperty]
	private LoginModel _model = new();

	public LoginViewModel(IShellService shellService, ILoginService loginService)
	{
		_shellService = shellService;
		_loginService = loginService;
	}

	[RelayCommand]
	private async Task GoToRegisterPage()
	{
		await _shellService.GoToAsync(AppShell.GetRoute<RegisterPage>());
	}

	[RelayCommand]
	private async Task Login()
	{
		if (await _loginService.LoginAsync(Model))
		{
			await _shellService.GoToAsync(AppShell.GetRoute<BillListPage>());
		}
		else
		{
			await Shell.Current.DisplayAlert("ValidationError", "User does not exist", "ok");
		}
	}
}