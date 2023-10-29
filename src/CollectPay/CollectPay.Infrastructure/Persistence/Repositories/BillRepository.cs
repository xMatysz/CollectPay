using CollectPay.Domain.BillAggregate;

namespace CollectPay.Infrastructure.Persistence.Repositories;

public sealed class BillRepository : Repository<Bill, Guid>
{
	public BillRepository(CollectPayDbContext dbContext)
		: base(dbContext)
	{
	}
}