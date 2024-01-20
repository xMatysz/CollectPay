using CollectPay.Application.Common.Repositories;
using CollectPay.Domain.UserAggregate;
using CollectPay.Persistence;

namespace CollectPay.Infrastructure.Repositories;

public class UserRepository : RepositoryBase, IUserRepository
{
	public UserRepository(CollectPayDbContext dbContext)
		: base(dbContext)
	{
	}

	public async Task AddAsync(User user, CancellationToken cancellationToken)
	{
		await DbContext.AddAsync(user, cancellationToken);
	}

	public async Task<User?> GetByIdAsync(Guid userId, CancellationToken cancellationToken)
	{
		return await DbContext.Set<User>().FindAsync([userId], cancellationToken: cancellationToken);
	}
}