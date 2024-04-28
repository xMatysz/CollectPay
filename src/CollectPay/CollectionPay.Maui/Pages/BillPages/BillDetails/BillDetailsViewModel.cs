﻿using CollectionPay.Maui.Abstraction;
using CollectionPay.Maui.Models;
using CollectionPay.Maui.Pages.PaymentPages.PaymentList;
using CollectionPay.Maui.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace CollectionPay.Maui.Pages.BillPages.BillDetails;

[QueryProperty("model", nameof(Bill))]
public partial class BillDetailsViewModel : ViewModelBase, IQueryAttributable
{
	private readonly IShellService _shellService;

	[ObservableProperty]
	private BillModel _bill;

	public BillDetailsViewModel(IShellService shellService)
	{
		_shellService = shellService;
	}

	public void ApplyQueryAttributes(IDictionary<string, object> query)
	{
		var item = query["model"] as BillModel ?? throw new InvalidCastException();
		Bill = item;
		Title = $"Edit {Bill.Name}";
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
}