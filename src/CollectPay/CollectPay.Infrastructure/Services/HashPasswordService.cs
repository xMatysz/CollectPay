using System.Security.Cryptography;
using System.Text;
using CollectPay.Application.Services;

namespace CollectPay.Infrastructure.Services;

public class HashPasswordService : IHashPasswordService
{
	public string HashPassword(string password)
	{
		var bytes = Encoding.UTF8.GetBytes(password);
		var hashData = SHA256.HashData(bytes);
		return Convert.ToHexString(hashData);
	}
}