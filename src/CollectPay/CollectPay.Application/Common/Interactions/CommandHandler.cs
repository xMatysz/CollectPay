using ErrorOr;
using MediatR;

namespace CollectPay.Application.Common.Interactions;

public abstract class CommandHandler<TCommand, TResult> : IRequestHandler<TCommand, TResult>
	where TCommand : ICommand<TResult>
	where TResult : IErrorOr
{
	public async Task<TResult> Handle(TCommand request, CancellationToken cancellationToken)
	{
		return await Process(request, cancellationToken);
	}

	protected abstract Task<TResult> Process(TCommand command, CancellationToken cancellationToken);
}