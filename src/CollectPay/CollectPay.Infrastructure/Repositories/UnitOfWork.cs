﻿using CollectPay.Application.Common.Repositories;
using CollectPay.Persistence;

namespace CollectPay.Infrastructure.Repositories;

public class UnitOfWork : IUnitOfWork
{
	private readonly CollectPayDbContext _dbContext;

	public UnitOfWork(CollectPayDbContext dbContext)
	{
		_dbContext = dbContext;
	}

	public void BeginTransaction()
	{
		_dbContext.Database.BeginTransaction();
	}

	public void RollbackTransaction()
	{
		_dbContext.Database.RollbackTransaction();
	}

	public async Task CommitAsync(CancellationToken cancellationToken)
	{
		await _dbContext.Database.CommitTransactionAsync(cancellationToken);
		await _dbContext.SaveChangesAsync(cancellationToken);
	}

	public void Dispose()
	{
		GC.SuppressFinalize(this);
		_dbContext.Dispose();
	}
}