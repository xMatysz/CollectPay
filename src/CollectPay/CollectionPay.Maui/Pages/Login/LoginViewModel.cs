using CollectionPay.Maui.Common;
using CollectionPay.Maui.Pages.Bills.BillList;
using CollectionPay.Maui.Pages.Register;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace CollectionPay.Maui.Pages.Login;

public partial class LoginViewModel : ViewModelBase
{
	[ObservableProperty]
	private LoginModel _model = new();

	private readonly IShellService _shellService;

	public LoginViewModel(IShellService shellService)
	{
		_shellService = shellService;
	}

	[ObservableProperty]
	private string _userName;

	[RelayCommand]
	public async Task Login()
	{
		await _shellService.GoToAsync(nameof(BillListView));
	}

	[RelayCommand]
	public async Task Register()
	{
		await _shellService.GoToAsync(nameof(RegisterView));
	}
}