namespace CollectPay.Application.Common.Repositories;

public interface IUnitOfWork : IDisposable
{
	void BeginTransaction();
	Task RollbackTransactionAsync();
	Task SaveChangesAsync(CancellationToken cancellationToken = default);
	Task FinishTransactionAsync(CancellationToken cancellationToken = default);
}