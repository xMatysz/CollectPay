using CollectionPay.Maui.Pages.BillPages.BillList;
using CollectionPay.Maui.Services;

namespace CollectionPay.Maui.Pages.LoginPages.Login;

public partial class LoginPage
{
	private readonly ILoginService _loginService;
	private readonly IShellService _shellService;

	public LoginPage(LoginViewModel viewModel, ILoginService loginService, IShellService shellService)
		: base(viewModel)
	{
		InitializeComponent();
		_loginService = loginService;
		_shellService = shellService;
	}

	protected override void OnNavigatedTo(NavigatedToEventArgs args)
	{
		base.OnNavigatedTo(args);

		var destination = _loginService.IsAuthenticated()
			? AppShell.GetRoute<BillListPage>()
			: AppShell.GetRoute<LoginPage>();

		_shellService.GoToAsync(destination);
	}
}