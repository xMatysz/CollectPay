using MediatR;

namespace CollectPay.Application.Common.Interactions;

public abstract class CommandHandler<TCommand> : IRequestHandler<TCommand>
	where TCommand : ICommand
{
	public async Task Handle(TCommand request, CancellationToken cancellationToken)
	{
		await Process(request, cancellationToken);
	}

	protected abstract Task Process(TCommand command, CancellationToken cancellationToken);
}

public abstract class CommandHandler<TCommand, TResult> : IRequestHandler<TCommand, TResult>
	where TCommand : ICommand<TResult>
	where TResult : CommandResult
{
	public async Task<TResult> Handle(TCommand request, CancellationToken cancellationToken)
	{
		return await Process(request, cancellationToken);
	}

	protected abstract Task<TResult> Process(TCommand command, CancellationToken cancellationToken);
}