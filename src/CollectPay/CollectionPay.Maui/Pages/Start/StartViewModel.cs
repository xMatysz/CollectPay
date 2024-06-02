using CollectionPay.Maui.Abstraction;
using CollectionPay.Maui.Pages.BillPages.BillList;
using CollectionPay.Maui.Pages.LoginPages.Login;
using CollectionPay.Maui.Services;

namespace CollectionPay.Maui.Pages.Start;

public class StartViewModel : ViewModelBase
{
	private readonly ILoginService _loginService;

	public StartViewModel(ILoginService loginService)
	{
		_loginService = loginService;
	}

	public async Task RedirectUser()
	{
		IsBusy = true;

		await Task.Delay(2000);
		var isAuth = await _loginService.IsAuthenticated();

		if (!isAuth)
		{
			await Shell.Current.GoToAsync(AppShell.GetRoute<LoginPage>());
			IsBusy = false;
			return;
		}

		await Shell.Current.GoToAsync(AppShell.GetRoute<BillListPage>());
		IsBusy = false;
	}
}