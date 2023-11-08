namespace CollectPay.Application.Common.Repositories;

public interface IUnitOfWork : IDisposable
{
	void BeginTransaction();
	void RollbackTransaction();
	Task CommitAsync(CancellationToken cancellationToken);
}