using System.Net;
using System.Net.Http.Json;
using CollectionPay.Contracts.Routes;
using CollectPay.Api.ApiTests.Controllers.BillControllerTests.Doubles;
using CollectPay.Application.BillAggregate.Commands.Create;
using CollectPay.Application.BillAggregate.Queries;
using CollectPay.Domain.BillAggregate;
using ErrorOr;

namespace CollectPay.Api.ApiTests.Controllers.BillControllerTests;

public sealed class WhenSendingRequestToBillController : ControllerTestBase
{
	[Fact]
	public async Task ShouldReturnListOfBills()
	{
		const string url = BillRoutes.List;
		ConfigureHandler<GetBillsQuery, List<Bill>, SuccessfulGetBillHandler>();

		var response = await Client.GetAsync(url);
		response.StatusCode.Should().Be(HttpStatusCode.OK);
	}

	[Fact]
	public async Task ShouldCreateBill()
	{
		const string url = BillRoutes.Create;
		var command = new CreateBillCommand(Guid.NewGuid(), "BillName", new List<Guid>());
		ConfigureHandler<CreateBillCommand, ErrorOr<Created>, SuccessfulCreateBillHandler>();

		var response = await Client.PostAsJsonAsync(url, command);

		response.StatusCode.Should().Be(HttpStatusCode.OK);
	}
}