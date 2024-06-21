using CollectPay.Application.Common.Repositories;
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

	public Task RollbackTransactionAsync()
	{
		return _dbContext.Database.RollbackTransactionAsync();
	}

	public async Task SaveChangesAsync(CancellationToken cancellationToken)
	{
		await _dbContext.SaveChangesAsync(cancellationToken);
	}

	public async Task FinishTransactionAsync(CancellationToken cancellationToken)
	{
		await _dbContext.Database.CommitTransactionAsync(cancellationToken);
		await SaveChangesAsync(cancellationToken);
	}

	public void Dispose()
	{
		GC.SuppressFinalize(this);
		_dbContext.Dispose();
	}
}