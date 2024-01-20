using CollectPay.Application.Common.Repositories;
using CollectPay.Domain.UserAggregate;
using MediatR;

namespace CollectPay.Application.UserAggregate.Queries.GetUserQuery;

public class GetUserQueryHandler : IRequestHandler<GetUserQuery, ErrorOr<User>>
{
	private readonly IUserRepository _userRepository;

	public GetUserQueryHandler(IUserRepository userRepository)
	{
		_userRepository = userRepository;
	}

	public async Task<ErrorOr<User>> Handle(GetUserQuery request, CancellationToken cancellationToken)
	{
		var result = await _userRepository.GetByIdAsync(request.UserId, cancellationToken);

		if (result is null)
		{
			return RepositoryErrors
				.EntityWithIdNotFound(nameof(User), request.UserId.ToString());
		}

		return result;
	}
}