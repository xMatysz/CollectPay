using CollectPay.Persistence;

namespace CollectPay.Infrastructure.Repositories;

public abstract class Repository
{
	protected readonly CollectPayDbContext DbContext;

	protected Repository(CollectPayDbContext dbContext)
	{
		DbContext = dbContext;
	}
}