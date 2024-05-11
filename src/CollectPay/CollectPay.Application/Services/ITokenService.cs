namespace CollectPay.Application.Services;

public interface ITokenService
{
	string GenerateToken(Guid userId);
}