namespace CollectPay.Application.Common.Repositories;

public interface IUnitOfWork : IDisposable
{
	void BeginTransaction();
	void RollbackTransaction();
	Task SaveChangesAsync(CancellationToken cancellationToken = default);
	Task CommitAsync(CancellationToken cancellationToken = default);
}