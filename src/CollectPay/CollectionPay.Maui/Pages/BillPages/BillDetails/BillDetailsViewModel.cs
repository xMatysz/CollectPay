using System.Collections.ObjectModel;
using CollectionPay.Contracts;
using CollectionPay.Contracts.Requests.Bill;
using CollectionPay.Maui.Abstraction;
using CollectionPay.Maui.Models;
using CollectionPay.Maui.Pages.BillPages.BillList;
using CollectionPay.Maui.Pages.PaymentPages.PaymentList;
using CollectionPay.Maui.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace CollectionPay.Maui.Pages.BillPages.BillDetails;

[QueryProperty("model", nameof(Bill))]
public partial class BillDetailsViewModel : ViewModelBase, IQueryAttributable
{
	private readonly IShellService _shellService;
	private readonly IApiClient _apiClient;

	public ObservableCollection<Guid> Debtors { get; } = new();

	[ObservableProperty]
	private BillModel _bill;

	[ObservableProperty]
	private string _userEmail;

	public BillDetailsViewModel(IShellService shellService, IApiClient apiClient)
	{
		_shellService = shellService;
		_apiClient = apiClient;
	}

	public void ApplyQueryAttributes(IDictionary<string, object> query)
	{
		var item = query["model"] as BillModel ?? throw new InvalidCastException();
		Bill = item;
		Title = $"Edit {Bill.Name}";

		foreach (var debtor in Bill.Debtors)
		{
			Debtors.Add(debtor);
		}
	}

	[RelayCommand]
	private async Task GoBack()
	{
		// Why it going back to main instead of 1 page back
		// await Shell.Current.GoToAsync("..");

		await _shellService.GoToAsync(AppShell.GetRoute<PaymentListPage>(), new Dictionary<string, object>
		{
			["model"] = Bill
		});
	}

	[RelayCommand]
	private async Task UpdateBill()
	{
		var request = new UpdateBillRequest(Bill.Id, debtorsEmailsToAdd: [UserEmail]);

		var response = await _apiClient.SendPost(BillRoutes.Update, request, CancellationToken.None);

		if (response.IsSuccessStatusCode)
		{
			await _shellService.GoToAsync(AppShell.GetRoute<BillListPage>());
			return;
		}

		await Shell.Current.DisplayAlert("Error", "Cant add user", "Ok");
	}
}