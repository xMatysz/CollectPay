using CollectPay.Persistence;

namespace CollectPay.Infrastructure.Repositories;

public abstract class RepositoryBase
{
	protected readonly CollectPayDbContext DbContext;

	protected RepositoryBase(CollectPayDbContext dbContext)
	{
		DbContext = dbContext;
	}
}