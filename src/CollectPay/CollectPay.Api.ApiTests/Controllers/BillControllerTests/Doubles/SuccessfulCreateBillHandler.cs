using CollectPay.Application.BillAggregate.Commands.Create;
using CollectPay.Application.Common.Interactions;
using ErrorOr;

namespace CollectPay.Api.ApiTests.Controllers.BillControllerTests.Doubles;

public class SuccessfulCreateBillHandler : CommandHandler<CreateBillCommand, ErrorOr<Created>>
{
	protected override Task<ErrorOr<Created>> Process(CreateBillCommand command, CancellationToken cancellationToken)
	{
		return Task.FromResult<ErrorOr<Created>>(Result.Created);
	}
}