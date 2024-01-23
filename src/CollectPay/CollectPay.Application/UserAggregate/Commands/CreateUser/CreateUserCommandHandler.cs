using CollectPay.Application.Common.Interactions;
using CollectPay.Application.Common.Repositories;
using CollectPay.Domain.UserAggregate;

namespace CollectPay.Application.UserAggregate.Commands.CreateUser;

public class CreateUserCommandHandler : CommandHandler<CreateUserCommand, ErrorOr<Created>>
{
	private readonly IUserRepository _userRepository;

	public CreateUserCommandHandler(IUserRepository userRepository)
	{
		_userRepository = userRepository;
	}

	protected override async Task<ErrorOr<Created>> Process(CreateUserCommand command, CancellationToken cancellationToken)
	{
		var user = User.Create(command.Email, command.Password, command.NickName);

		if (user.IsError)
		{
			return user.Errors;
		}

		await _userRepository.AddAsync(user.Value, cancellationToken);
		return Result.Created;
	}
}