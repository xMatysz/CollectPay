using System.Collections.ObjectModel;
using CollectionPay.Maui.Abstraction;
using CollectionPay.Maui.Models;
using CollectionPay.Maui.Pages.PaymentPages.PaymentCreate;
using CollectionPay.Maui.Pages.PaymentPages.PaymentDetails;
using CollectionPay.Maui.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace CollectionPay.Maui.Pages.PaymentPages.PaymentList;

public partial class PaymentListViewModel : ViewModelBase, IHaveDataToLoad, IQueryAttributable
{
	private readonly IDispatcher _dispatcher;
	private readonly IShellService _shellService;

	[ObservableProperty]
	private BillModel _bill;

	public bool IsDataLoaded => Payments.Any();

	public ObservableCollection<PaymentModel> Payments { get; } = new();

	public PaymentListViewModel(IDispatcher dispatcher, IShellService shellService)
	{
		_dispatcher = dispatcher;
		_shellService = shellService;
	}

	public void ApplyQueryAttributes(IDictionary<string, object> query)
	{
		Bill = (BillModel)query["model"];
		Title = Bill.Name;
	}

	[RelayCommand]
	private async Task Refresh()
	{
		IsRefreshing = true;

		await LoadPayments();

		IsRefreshing = false;
	}


	[RelayCommand]
	private async Task GoToPaymentDetails(PaymentModel model)
	{
		await _shellService.GoToAsync(AppShell.GetRoute<PaymentDetailsPage>(), new Dictionary<string, object>
		{
			["model"] = model
		});
	}

	[RelayCommand]
	private async Task GoToPaymentCreate()
	{
		await _shellService.GoToAsync(AppShell.GetRoute<PaymentCreatePage>());
	}

	private async Task LoadPayments()
	{
		IsBusy = true;

		await _dispatcher.DispatchAsync(Payments.Clear);

		var photo = "https://png.pngtree.com/png-vector/20230523/ourmid/pngtree-money-bag-vector-png-image_7106786.png";

		var payments = new[]
		{
			new PaymentModel(photo,"New Payment1", 21.37m, "USD"),
			new PaymentModel(photo,"New Payment2", 21.37m, "USD"),
			new PaymentModel(photo,"New Payment3", 21.37m, "USD"),
		};

		foreach (var payment in payments)
		{
			await _dispatcher.DispatchAsync(() => Payments.Add(payment));
		}

		IsBusy = false;
	}
}