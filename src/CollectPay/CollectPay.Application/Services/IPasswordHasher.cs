namespace CollectPay.Application.Services;

public interface IPasswordHasher
{
	(byte[] Salt, byte[] HashedString) HashString(string value);
	bool ValidateHash(string value, byte[] hashResultHashedString, byte[] hashResultSalt);
}