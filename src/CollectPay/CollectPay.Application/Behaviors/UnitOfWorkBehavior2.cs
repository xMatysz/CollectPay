using CollectPay.Application.Common.Interactions;
using CollectPay.Application.Common.Repositories;
using MediatR;

namespace CollectPay.Application.Behaviors;

public sealed class UnitOfWorkBehavior<TCommand,TResult> : IPipelineBehavior<TCommand,TResult>
	where TCommand : ICommand, ICommand<TResult>
	where TResult : CommandResult
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
			return result;
		}
		catch
		{
			_unitOfWork.RollbackTransaction();
		}
		finally
		{
			await _unitOfWork.CommitAsync(cancellationToken);
		}

		return default!;
	}
}

public sealed class UnitOfWorkBehavior2<TCommand, _> : IPipelineBehavior<TCommand, Unit>
	where TCommand : ICommand
{
	private readonly IUnitOfWork _unitOfWork;

	public UnitOfWorkBehavior2(IUnitOfWork unitOfWork)
	{
		_unitOfWork = unitOfWork;
	}

	public async Task<Unit> Handle(TCommand request, RequestHandlerDelegate<Unit> next, CancellationToken cancellationToken)
	{
		_unitOfWork.BeginTransaction();

		try
		{
			await next();
		}
		catch
		{
			_unitOfWork.RollbackTransaction();
		}
		finally
		{
			await _unitOfWork.CommitAsync(cancellationToken);
		}

		return Unit.Value;
	}
}

public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : notnull
{
	public Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
	{
		throw new NotImplementedException();
	}
}