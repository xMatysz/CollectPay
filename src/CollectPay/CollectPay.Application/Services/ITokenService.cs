namespace CollectPay.Application.Services;

public interface ITokenService
{
	string GenerateToken(string email);
}