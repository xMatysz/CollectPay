using System.Net.Http.Json;
using CollectionPay.Contracts.Requests;
using CollectionPay.Contracts.Routes;
using CollectionPay.Maui.Pages.Bills.BillList;

namespace CollectionPay.Maui.Services;

public interface IBillService
{
	Task<BillModel[]> GetAllBillsAsync(CancellationToken cancellationToken = default);
	Task CreateBillAsync(BillModel bill, CancellationToken cancellationToken = default);
}

public class BillService : ServiceBase, IBillService
{
	public BillService(HttpClient client)
		: base(client)
	{
	}

	public async Task<BillModel[]> GetAllBillsAsync(CancellationToken cancellationToken = default)
	{
		return await HttpClient.GetFromJsonAsync<BillModel[]>(BillRoutes.List, cancellationToken);
	}

	public Task CreateBillAsync(BillModel bill, CancellationToken cancellationToken = default)
	{
		var request = new CreateBillRequest(bill.CreatorId, bill.Name);
		return HttpClient.PostAsJsonAsync(BillRoutes.Create, request, cancellationToken);
	}
}