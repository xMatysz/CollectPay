using CollectPay.Application.Common.Interactions;
using CollectPay.Application.Common.Repositories;
using MediatR;

namespace CollectPay.Application.Behaviors;

public sealed class UnitOfWorkBehavior<TCommand, _> : IPipelineBehavior<TCommand, Unit>
	where TCommand : ICommand
{
	private readonly IUnitOfWork _unitOfWork;

	public UnitOfWorkBehavior(IUnitOfWork unitOfWork)
	{
		_unitOfWork = unitOfWork;
	}

	public async Task<Unit> Handle(TCommand request, RequestHandlerDelegate<Unit> next, CancellationToken cancellationToken)
	{
		_unitOfWork.BeginTransaction();

		try
		{
			await next();
			await _unitOfWork.CommitAsync(cancellationToken);
		}
		catch
		{
			_unitOfWork.RollbackTransaction();
		}

		return Unit.Value;
	}
}