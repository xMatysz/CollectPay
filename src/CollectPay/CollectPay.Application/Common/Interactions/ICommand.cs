using MediatR;

namespace CollectPay.Application.Common.Interactions;

public interface ICommand : IRequest
{
}

public interface ICommand<out TResult> : IRequest<TResult>
	where TResult : CommandResult
{
}