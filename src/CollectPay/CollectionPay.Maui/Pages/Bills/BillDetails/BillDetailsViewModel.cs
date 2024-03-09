using System.Collections.ObjectModel;
using CollectionPay.Maui.Common;
using CollectionPay.Maui.Pages.Bills.BillList;
using CollectionPay.Maui.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace CollectionPay.Maui.Pages.Bills.BillDetails;

[QueryProperty(nameof(Bill), "bill")]
public partial class BillDetailsViewModel : ViewModelBase
{
	[ObservableProperty]
	private BillModel _bill;

	public ObservableCollection<PaymentModel> Payments { get; } = new();

	private readonly IBillService _billService;

	public BillDetailsViewModel(IBillService billService)
	{
		_billService = billService;
	}

	[RelayCommand]
	private async Task GetPayments()
	{
		await OnRefreshingAsync(async () =>
		{
			var payments = await _billService.GetAllPaymentsForBill(Bill.Id);

			Payments.Clear();

			foreach (var payment in payments)
			{
				Payments.Add(payment);
			}
		});
	}
}