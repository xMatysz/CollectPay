using System.Net;
using CollectionPay.Contracts.Routes;
using CollectPay.Api.ApiTests.Common.Doubles;
using CollectPay.Application.BillAggregate.Queries.GetPayments;
using CollectPay.Domain.BillAggregate.Entities;

namespace CollectPay.Api.ApiTests.Controllers.PaymentControllerTests;

public class WhenSendingRequestToPaymentController : ControllerTestBase
{
	[Fact]
	public async Task ShouldReturnListOfBills()
	{
		const string url = PaymentRoutes.List;
		ConfigureHandler<GetPaymentsQuery, Payment[], SuccessFullHandler<GetPaymentsQuery, Payment[]>>();

		var response = await Client.GetAsync(url);
		response.StatusCode.Should().Be(HttpStatusCode.OK);
	}
}