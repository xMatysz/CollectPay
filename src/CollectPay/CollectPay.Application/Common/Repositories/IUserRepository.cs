using CollectPay.Domain.UserAggregate;

namespace CollectPay.Application.Common.Repositories;

public interface IUserRepository
{
	Task AddAsync(User user, CancellationToken cancellationToken);
	Task<User?> GetByEmail(string email, CancellationToken cancellationToken);
}