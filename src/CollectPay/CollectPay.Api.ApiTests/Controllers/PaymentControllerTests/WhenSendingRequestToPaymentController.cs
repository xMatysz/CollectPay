using System.Net;
using CollectionPay.Contracts.Routes;
using CollectPay.Api.ApiTests.Common.Doubles;
using CollectPay.Application.BillAggregate.Queries.GetPayments;
using CollectPay.Application.Common.Abstraction;
using CollectPay.Domain.BillAggregate.Entities;
using ErrorOr;

namespace CollectPay.Api.ApiTests.Controllers.PaymentControllerTests;

public class WhenSendingRequestToPaymentController : ControllerTestBase
{
	[Fact]
	public async Task ShouldReturnListOfBills()
	{
		const string url = PaymentRoutes.List;
		ConfigureHandler<GetPaymentsQuery, ErrorOr<Payment[]>, SuccessFullHandler<GetPaymentsQuery, ErrorOr<Payment[]>>>();

		var response = await Client.GetAsync(url);
		response.StatusCode.Should().Be(HttpStatusCode.OK);
	}

	[Fact]
	public async Task ShouldCreatePayment()
	{
		const string url = PaymentRoutes.Create;
		ConfigureHandler<CreatePaymentCommand, ErrorOr<Created>, SuccessFullHandler<CreatePaymentCommand, ErrorOr<Created>>>();

		var response = await Client.GetAsync(url);
		response.StatusCode.Should().Be(HttpStatusCode.OK);
	}

	[Fact]
	public async Task ShouldUpdatePayment()
	{
		const string url = PaymentRoutes.Update;
		ConfigureHandler<UpdatePaymentCommand, ErrorOr<Updated>, SuccessFullHandler<UpdatePaymentCommand, ErrorOr<Updated>>>();

		var response = await Client.GetAsync(url);
		response.StatusCode.Should().Be(HttpStatusCode.OK);
	}

	[Fact]
	public async Task ShouldUpdateRemove()
	{
		const string url = PaymentRoutes.Remove;
		ConfigureHandler<RemovePaymentCommand, ErrorOr<Deleted>, SuccessFullHandler<RemovePaymentCommand, ErrorOr<Deleted>>>();

		var response = await Client.GetAsync(url);
		response.StatusCode.Should().Be(HttpStatusCode.OK);
	}
}

public record CreatePaymentCommand(Guid BillId) : ICommand<Created>;
public record UpdatePaymentCommand(Guid BillId, Guid PaymentId) : ICommand<Updated>;
public record RemovePaymentCommand(Guid BillId, Guid PaymentId) : ICommand<Deleted>;