using ErrorOr;

namespace CollectPay.Api.ApiTests.Common.Doubles;

public class SuccessFullHandler<TRequest, TResult> :
	IRequestHandler<TRequest, TResult>
	where TRequest : IRequest<TResult>
{
	public Task<TResult> Handle(TRequest request, CancellationToken cancellationToken)
	{
		return Task.FromResult<TResult>(default!);
	}
}