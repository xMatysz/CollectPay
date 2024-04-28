using CollectPay.Application.Common.Abstraction;
using CollectPay.Application.Common.Repositories;
using CollectPay.Application.Services;
using CollectPay.Domain.UserAggregate;
using CollectPay.Domain.UserAggregate.Errors;
using ErrorOr;

namespace CollectPay.Application.UserAggregate.Register;

public class RegisterUserCommandHandler : ICommandHandler<RegisterUserCommand, User>
{
	private readonly IPasswordHasher _hasher;
	private readonly IUserRepository _userRepository;

	public RegisterUserCommandHandler(IPasswordHasher hasher, IUserRepository userRepository)
	{
		_hasher = hasher;
		_userRepository = userRepository;
	}

	public async Task<ErrorOr<User>> Handle(RegisterUserCommand command, CancellationToken cancellationToken)
	{
		var existingUser = await _userRepository.GetByEmail(command.Email, cancellationToken);

		if (existingUser is not null)
		{
			return UserErrors.UserAlreadyExist;
		}

		var hashResult = _hasher.HashString(command.Password);

		var createUserResult = User.Create(command.Email, hashResult.HashedString, hashResult.Salt);

		if (createUserResult.IsError)
		{
			return createUserResult.Errors;
		}

		await _userRepository.AddAsync(createUserResult.Value, cancellationToken);

		return createUserResult.Value;
	}
}