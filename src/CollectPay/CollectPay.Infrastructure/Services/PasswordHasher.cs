using System.Security.Cryptography;
using System.Text;
using CollectPay.Application.Services;

namespace CollectPay.Infrastructure.Services;

public class PasswordHasher : IPasswordHasher
{
	public (byte[] Salt, byte[] HashedString) HashString(string value)
	{
		using var hmac = new HMACSHA512();

		var salt = hmac.Key;
		var hashedString = hmac.ComputeHash(Encoding.UTF8.GetBytes(value));

		return (salt, hashedString);
	}

	public bool ValidateHash(string value, byte[] hashedString, byte[] salt)
	{
		using var hmac = new HMACSHA512(salt);

		var result = hmac.ComputeHash(Encoding.UTF8.GetBytes(value));

		return result.SequenceEqual(hashedString);
	}
}