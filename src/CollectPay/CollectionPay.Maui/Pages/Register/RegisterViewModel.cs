using CollectionPay.Maui.Common;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace CollectionPay.Maui.Pages.Register;

public partial class RegisterViewModel : ViewModelBase
{
	[ObservableProperty]
	private RegisterModel _model = new();

	public RegisterViewModel( )
	{

	}
	[RelayCommand]
	public async Task SendRegisterRequest()
	{
	}
}