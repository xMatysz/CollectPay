using ErrorOr;
using MediatR;

namespace CollectPay.Application.Common.Abstraction;

public interface ICommandHandler<in TCommand, TResult> : IRequestHandler<TCommand, ErrorOr<TResult>>
	where TCommand : ICommand<ErrorOr<TResult>>;