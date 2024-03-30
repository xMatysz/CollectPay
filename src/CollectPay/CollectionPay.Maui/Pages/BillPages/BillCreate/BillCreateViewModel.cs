using CollectionPay.Maui.Abstraction;
using CollectionPay.Maui.Models;
using CollectionPay.Maui.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace CollectionPay.Maui.Pages.BillPages.BillCreate;

public partial class BillCreateViewModel : ViewModelBase
{
	private readonly IShellService _shellService;

	[ObservableProperty]
	private BillCreateModel _model = new();

	public BillCreateViewModel(IShellService shellService)
	{
		_shellService = shellService;
	}

	[RelayCommand]
	private void Clear()
	{
		Model.Image = null;
		Model.Name = null;
		OnPropertyChanged(nameof(Model));
	}

	[RelayCommand]
	private async Task CreateBill()
	{
		await _shellService.GoToAsync("..");
	}
}