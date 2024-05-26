using CollectPay.Application.Common.Abstraction;
using CollectPay.Application.Common.Repositories;
using CollectPay.Application.Services;
using CollectPay.Domain.UserAggregate.Errors;
using ErrorOr;

namespace CollectPay.Application.UserAggregate.Login;

public class LoginUserQueryHandler : IQueryHandler<LoginUserQuery, LoginDto>
{
	private readonly IUserRepository _userRepository;
	private readonly IPasswordHasher _passwordHasher;
	private readonly ITokenService _tokenService;

	public LoginUserQueryHandler(IUserRepository userRepository, IPasswordHasher passwordHasher, ITokenService tokenService)
	{
		_userRepository = userRepository;
		_passwordHasher = passwordHasher;
		_tokenService = tokenService;
	}

	public async Task<ErrorOr<LoginDto>> Handle(LoginUserQuery query, CancellationToken cancellationToken)
	{
		var user = await _userRepository.GetByEmail(query.Email, cancellationToken);

		if (user is null)
		{
			return UserErrors.InvalidCredentials;
		}

		var bytePassword = (byte[])user.Password;
		var byteSalt = (byte[])user.PasswordSalt;

		var isValid = _passwordHasher.ValidateHash(query.Password, bytePassword, byteSalt);
		var token = _tokenService.GenerateToken(user.Id);
		var response = new LoginDto(token, user.Email);

		return isValid
			? response
			: UserErrors.InvalidCredentials;
	}
}