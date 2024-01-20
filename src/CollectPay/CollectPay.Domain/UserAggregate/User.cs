using System.Text.RegularExpressions;
using CollectPay.Domain.Common.Models;
using CollectPay.Domain.UserAggregate.Errors;

namespace CollectPay.Domain.UserAggregate;

public class User : AggregateRoot
{
	public string Email { get; set; }
	public string Password { get; set; }
	public string Nick { get; set; }

	private User(string email, string password, string nick)
	{
		Email = email;
		Password = password;
		Nick = nick;
	}

	public static ErrorOr<User> Create(string email, string password, string nick)
	{
		if (!IsValidEmail(email))
		{
			return UserErrors.InvalidEmail;
		}

		return new User(email, password, nick);
	}

	private static bool IsValidEmail(string email)
	{
		const string emailRegex = "^[\\w-\\.]+@([\\w-]+\\.)+[\\w-]{2,4}$";
		var regex = new Regex(emailRegex);

		return regex.IsMatch(email);
	}
}
