using System.Net;
using System.Net.Http.Json;
using CollectionPay.Contracts.Requests.Bill;
using CollectionPay.Contracts.Routes;
using CollectPay.Api.ApiTests.Common.Doubles;
using CollectPay.Application.BillAggregate.Commands.Bills.CreateBill;
using CollectPay.Application.BillAggregate.Queries.GetBills;
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

		response.StatusCode.Should().Be(HttpStatusCode.OK);
	}
}