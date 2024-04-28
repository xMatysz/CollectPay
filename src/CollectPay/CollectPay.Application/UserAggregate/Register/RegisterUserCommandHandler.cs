using CollectPay.Application.Common.Abstraction;
using CollectPay.Application.Services;
using CollectPay.Domain.UserAggregate;
using ErrorOr;

namespace CollectPay.Application.UserAggregate.Register;

public class RegisterUserCommandHandler : ICommandHandler<RegisterUserCommand, User>
{
	private readonly IPasswordHasher _hasher;

	public RegisterUserCommandHandler(IPasswordHasher hasher)
	{
		_hasher = hasher;
	}

	public Task<ErrorOr<User>> Handle(RegisterUserCommand command, CancellationToken cancellationToken)
	{
		var hashResult = _hasher.HashString(command.Password);

		return Task.FromResult(User.Create(command.Email, hashResult.HashedString, hashResult.Salt));
	}
}