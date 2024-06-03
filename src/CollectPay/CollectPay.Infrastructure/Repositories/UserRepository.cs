using CollectPay.Application.Common.Repositories;
using CollectPay.Domain.UserAggregate;
using CollectPay.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CollectPay.Infrastructure.Repositories;

public class UserRepository : Repository, IUserRepository
{

	public UserRepository(CollectPayDbContext dbContext)
		: base(dbContext)
	{
	}

	public async Task AddAsync(User user, CancellationToken cancellationToken)
	{
		await DbContext.AddAsync(user, cancellationToken);
	}

	public Task<User?> GetByEmail(string email, CancellationToken cancellationToken)
	{
		return DbContext.Set<User>().FirstOrDefaultAsync(x => x.Email == email, cancellationToken);
	}

	public Task<User[]> GetByEmail(string[] emails, CancellationToken cancellationToken)
	{
		return DbContext.Set<User>().Where(x => emails.Contains(x.Email)).ToArrayAsync(cancellationToken);
	}
}