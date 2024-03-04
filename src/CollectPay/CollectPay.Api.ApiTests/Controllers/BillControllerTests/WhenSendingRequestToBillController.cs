using System.Net;
using System.Net.Http.Json;
using CollectionPay.Contracts.Requests.Bill;
using CollectionPay.Contracts.Routes;
using CollectPay.Api.ApiTests.Common.Doubles;
using CollectPay.Application.BillAggregate.Commands.Bills.CreateBill;
using CollectPay.Application.BillAggregate.Commands.Bills.RemoveBill;
using CollectPay.Application.BillAggregate.Commands.Bills.UpdateBill;
using CollectPay.Application.BillAggregate.Queries.Bills.GetBills;
using CollectPay.Domain.BillAggregate;
using ErrorOr;

namespace CollectPay.Api.ApiTests.Controllers.BillControllerTests;

public sealed class WhenSendingRequestToBillController : ControllerTestBase
{
	[Fact]
	public async Task ShouldReturnListOfBills()
	{
		const string url = BillRoutes.List;
		ConfigureHandler<GetBillsQuery, ErrorOr<List<Bill>>, SuccessFullHandler<GetBillsQuery, ErrorOr<List<Bill>>>>();

		var response = await Client.GetAsync(url);
		response.StatusCode.Should().BeOneOf(HttpStatusCode.OK, HttpStatusCode.NoContent);
	}

	[Fact]
	public async Task ShouldCreateBill()
	{
		const string url = BillRoutes.Create;
		var request = new CreateBillRequest(Guid.NewGuid(), "BillName");
		ConfigureHandler<CreateBillCommand, ErrorOr<Created>, SuccessFullHandler<CreateBillCommand, ErrorOr<Created>>>();

		var response = await Client.PostAsJsonAsync(url, request);

		response.StatusCode.Should().Be(HttpStatusCode.Created);
	}


	[Fact]
	public async Task ShouldUpdateBill()
	{
		const string url = BillRoutes.Update;
		var request = new UpdateBillRequest(Guid.NewGuid(), "TestName");
		ConfigureHandler<UpdateBillCommand, ErrorOr<Updated>, SuccessFullHandler<UpdateBillCommand, ErrorOr<Updated>>>();

		var response = await Client.PutAsJsonAsync(url, request);

		response.StatusCode.Should().Be(HttpStatusCode.OK);
	}


	[Fact]
	public async Task ShouldRemoveBill()
	{
		const string url = BillRoutes.Remove;
		ConfigureHandler<RemoveBillCommand, ErrorOr<Deleted>, SuccessFullHandler<RemoveBillCommand, ErrorOr<Deleted>>>();

		var response = await Client.DeleteAsync(url);

		response.StatusCode.Should().Be(HttpStatusCode.OK);
	}
}