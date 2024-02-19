namespace CollectPay.Application.Services;

public interface IHashPasswordService
{
	string HashPassword(string password);
}