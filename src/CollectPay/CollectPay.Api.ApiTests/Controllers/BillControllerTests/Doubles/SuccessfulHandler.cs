using CollectPay.Application.Common.Interactions;
using ErrorOr;

namespace CollectPay.Api.ApiTests.Controllers.BillControllerTests.Doubles;

public class SuccessfulHandler<TCommand, TResult> : CommandHandler<TCommand, TResult>
	where TCommand : ICommand<TResult>
	where TResult : IErrorOr
{
	protected override Task<TResult> Process(TCommand command, CancellationToken cancellationToken)
	{
		return Task.FromResult<TResult>(default!);
	}
}