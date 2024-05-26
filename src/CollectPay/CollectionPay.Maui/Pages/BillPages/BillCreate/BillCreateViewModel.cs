using CollectionPay.Contracts.Requests.Bill;
using CollectionPay.Contracts.Routes;
using CollectionPay.Maui.Abstraction;
using CollectionPay.Maui.Models;
using CollectionPay.Maui.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace CollectionPay.Maui.Pages.BillPages.BillCreate;

public partial class BillCreateViewModel : ViewModelBase
{
	private readonly IShellService _shellService;
	private readonly IApiClient _apiClient;

	[ObservableProperty]
	private BillCreateModel _model = new();

	public BillCreateViewModel(IShellService shellService, IApiClient apiClient)
	{
		_shellService = shellService;
		_apiClient = apiClient;

		Title = "Create Bill";
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
		var request = new CreateBillRequest(Model.Name);

		var response = await _apiClient.SendPost(BillRoutes.Create, request, CancellationToken.None);

		if (response.IsSuccessStatusCode)
		{
			await _shellService.GoToAsync("..");
			return;
		}

		await Shell.Current.DisplayAlert("Error", "Something wrong", "Ok");
	}
}