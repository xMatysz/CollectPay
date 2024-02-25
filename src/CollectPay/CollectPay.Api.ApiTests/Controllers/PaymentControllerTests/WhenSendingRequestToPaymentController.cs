using System.Net;
using System.Net.Http.Json;
using CollectionPay.Contracts.Requests.Payment;
using CollectionPay.Contracts.Routes;
using CollectPay.Api.ApiTests.Common.Doubles;
using CollectPay.Application.BillAggregate.Commands.Payments.CreatePayment;
using CollectPay.Application.BillAggregate.Commands.Payments.RemovePayment;
using CollectPay.Application.BillAggregate.Commands.Payments.UpdatePayment;
using CollectPay.Application.BillAggregate.Queries.Payments.GetPayments;
using CollectPay.Domain.BillAggregate.Entities;
using ErrorOr;

namespace CollectPay.Api.ApiTests.Controllers.PaymentControllerTests;

public class WhenSendingRequestToPaymentController : ControllerTestBase
{
	[Fact]
	public async Task ShouldReturnListOfPayments()
	{
		const string url = PaymentRoutes.List;
		ConfigureHandler<GetPaymentsQuery, ErrorOr<Payment[]>, SuccessFullHandler<GetPaymentsQuery, ErrorOr<Payment[]>>>();

		var response = await Client.GetAsync(url);
		response.StatusCode.Should().BeOneOf(HttpStatusCode.OK, HttpStatusCode.NoContent);
	}

	[Fact]
	public async Task ShouldCreatePayment()
	{
		const string url = PaymentRoutes.Create;
		var request = new CreatePaymentRequest(Guid.NewGuid(), Guid.NewGuid(), true, 21.23m, "USD", []);
		ConfigureHandler<CreatePaymentCommand, ErrorOr<Created>, SuccessFullHandler<CreatePaymentCommand, ErrorOr<Created>>>();

		var response = await Client.PostAsJsonAsync(url, request);

		response.StatusCode.Should().Be(HttpStatusCode.Created);
	}

	[Fact]
	public async Task ShouldUpdatePayment()
	{
		const string url = PaymentRoutes.Update;
		ConfigureHandler<UpdatePaymentCommand, ErrorOr<Updated>, SuccessFullHandler<UpdatePaymentCommand, ErrorOr<Updated>>>();
		var request = new UpdatePaymentRequest(Guid.NewGuid());

		var response = await Client.PutAsJsonAsync(url, request);
		response.StatusCode.Should().Be(HttpStatusCode.OK);
	}

	[Fact]
	public async Task ShouldRemovePayment()
	{
		const string url = PaymentRoutes.Remove;
		ConfigureHandler<RemovePaymentCommand, ErrorOr<Deleted>, SuccessFullHandler<RemovePaymentCommand, ErrorOr<Deleted>>>();

		var response = await Client.DeleteAsync(url);
		response.StatusCode.Should().Be(HttpStatusCode.OK);
	}
}