using System.Text.RegularExpressions;
using CollectPay.Domain.Common.Models;
using CollectPay.Domain.UserAggregate.Errors;
using ErrorOr;

namespace CollectPay.Domain.UserAggregate;

public sealed class User : AggregateRoot
{
	private const string _emailRegex = @"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$";

	public string Email { get; set; }
	public object Password { get; set; }
	public object PasswordSalt { get; set; }

	private User(string email, object password, object passwordSalt)
	{
		Email = email;
		Password = password;
		PasswordSalt = passwordSalt;
	}

	public static ErrorOr<User> Create(string email, object password, object passwordSalt)
	{
		if (!IsValidEmail(email))
		{
			return UserErrors.InvalidEmail;
		}

		return new User(email, password, passwordSalt);
	}

	private static bool IsValidEmail(string email)
		=> new Regex(_emailRegex)
			.IsMatch(email);
}