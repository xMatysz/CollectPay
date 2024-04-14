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

	[ObservableProperty]
	private LoginModel _model = new();

	public LoginViewModel(IShellService shellService)
	{
		_shellService = shellService;
	}

	[RelayCommand]
	private async Task GoToRegisterPage()
	{
		await _shellService.GoToAsync(AppShell.GetRoute<RegisterPage>());
	}

	[RelayCommand]
	private async Task Login()
	{
		if (Model is { Login: "admin", Password: "admin" })
		{
			await _shellService.GoToAsync(AppShell.GetRoute<BillListPage>());
		}
		else
		{
			await Shell.Current.DisplayAlert("ValidationError", "User does not exist", "ok");
		}
	}
}