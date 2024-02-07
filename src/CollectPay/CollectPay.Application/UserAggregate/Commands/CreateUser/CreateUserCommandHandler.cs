using CollectPay.Application.Common.Interactions;
using CollectPay.Application.Common.Repositories;
using CollectPay.Application.Services;
using CollectPay.Domain.UserAggregate;

namespace CollectPay.Application.UserAggregate.Commands.CreateUser;

public class CreateUserCommandHandler : CommandHandler<CreateUserCommand, ErrorOr<CreateUserCommandResult>>
{
	private readonly IUserRepository _userRepository;
	private readonly IUnitOfWork _unitOfWork;
	private readonly IHashPasswordService _hashPasswordService;

	public CreateUserCommandHandler(IUserRepository userRepository, IUnitOfWork unitOfWork, IHashPasswordService hashPasswordService)
	{
		_userRepository = userRepository;
		_unitOfWork = unitOfWork;
		_hashPasswordService = hashPasswordService;
	}

	protected override async Task<ErrorOr<CreateUserCommandResult>> Process(CreateUserCommand command, CancellationToken cancellationToken)
	{
		var newPassword = _hashPasswordService.HashPassword(command.Password);
		var user = User.Create(command.Email, newPassword, command.NickName);

		if (user.IsError)
		{
			return user.Errors;
		}

		await _userRepository.AddAsync(user.Value, cancellationToken);
		await _unitOfWork.SaveChangesAsync(cancellationToken);
		return new CreateUserCommandResult(user.Value.Id);
	}
}