﻿using System.Net.Http.Json;
using CollectionPay.Contracts.Requests.Bill;
using CollectionPay.Contracts.Routes;
using CollectionPay.Maui.Pages.Bills.BillDetails;
using CollectionPay.Maui.Pages.Bills.BillList;

namespace CollectionPay.Maui.Services;

public interface IBillService
{
	Task<BillModel[]> GetAllBillsAsync(CancellationToken cancellationToken = default);
	Task CreateBillAsync(BillModel bill, CancellationToken cancellationToken = default);
	Task<PaymentModel[]> GetAllPaymentsForBill(Guid billId, CancellationToken cancellationToken = default);
	Task DeleteBillAsync(Guid modelId, CancellationToken cancellationToken = default);
}

public class BillService : IBillService
{
	private readonly HttpClient _httpClient;

	public BillService(HttpClient client)
	{
		_httpClient = client;
	}

	public async Task<BillModel[]> GetAllBillsAsync(CancellationToken cancellationToken = default)
	{
		return await _httpClient.GetFromJsonAsync<BillModel[]>(BillRoutes.List, cancellationToken);
	}

	public Task CreateBillAsync(BillModel bill, CancellationToken cancellationToken = default)
	{
		var request = new CreateBillRequest(bill.CreatorId, bill.Name);
		return _httpClient.PostAsJsonAsync(BillRoutes.Create, request, cancellationToken);
	}

	public async Task<PaymentModel[]> GetAllPaymentsForBill(Guid billId, CancellationToken cancellationToken = default)
	{
		return await _httpClient.GetFromJsonAsync<PaymentModel[]>($"{PaymentRoutes.List}?billId={billId}", cancellationToken);
	}

	public async Task DeleteBillAsync(Guid billId, CancellationToken cancellationToken = default)
	{
		await _httpClient.DeleteAsync($"{BillRoutes.Remove}?billId={billId}", cancellationToken);
	}
}