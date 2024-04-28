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

	public Task<User?> GetByEmail(string email, CancellationToken cancellationToken)
	{
		return DbContext.Set<User>().FirstOrDefaultAsync(x => x.Email == email, cancellationToken);
	}
}