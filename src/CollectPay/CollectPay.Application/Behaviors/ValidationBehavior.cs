using CollectPay.Application.Common.Interactions;
using MediatR;

namespace CollectPay.Application.Behaviors;

public class ValidationBehavior<TCommand, _> : IPipelineBehavior<TCommand, Unit>
	where TCommand : ICommand
{
	public Task<Unit> Handle(TCommand request, RequestHandlerDelegate<Unit> next, CancellationToken cancellationToken)
	{
		return next();
	}
}