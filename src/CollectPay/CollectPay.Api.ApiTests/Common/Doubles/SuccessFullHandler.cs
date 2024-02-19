namespace CollectPay.Api.ApiTests.Common.Doubles;

public sealed class SuccessFullHandler<TCommand, TResult> : IRequestHandler<TCommand, TResult>
	where TCommand : IRequest<TResult>
{
	public Task<TResult> Handle(TCommand request, CancellationToken cancellationToken)
	{
		return Task.FromResult<TResult>(default!);
	}
}