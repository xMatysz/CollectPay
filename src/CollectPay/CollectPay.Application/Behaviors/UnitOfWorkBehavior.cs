using CollectPay.Application.Common.Abstraction;
using CollectPay.Application.Common.Repositories;
using ErrorOr;
using MediatR;

namespace CollectPay.Application.Behaviors;

public sealed class UnitOfWorkBehavior<TCommand, TResult> : IPipelineBehavior<TCommand, TResult>
	where TCommand : ICommand<TResult>
	where TResult : IErrorOr
{
	private readonly IUnitOfWork _unitOfWork;

	public UnitOfWorkBehavior(IUnitOfWork unitOfWork)
	{
		_unitOfWork = unitOfWork;
	}

	public async Task<TResult> Handle(TCommand request, RequestHandlerDelegate<TResult> next, CancellationToken cancellationToken)
	{
		_unitOfWork.BeginTransaction();

		try
		{
			var result = await next();

			if (!result.IsError)
			{
				await _unitOfWork.FinishTransactionAsync(cancellationToken);
			}
			else
			{
				await OnFailure();
			}

			return result;
		}
		catch
		{
			await OnFailure();
			throw;
		}
	}

	private Task OnFailure()
	{
		return _unitOfWork.RollbackTransactionAsync();
	}
}