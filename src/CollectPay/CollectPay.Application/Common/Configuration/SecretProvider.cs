namespace CollectPay.Application.Common.Configuration;

public class SecretProvider
{
	public JwtOptions Jwt { get; set; }
}

public class JwtOptions
{
	public string TokenKey { get; set; } = string.Empty;
	public string Issuer { get; set; } = string.Empty;
	public string Audience { get; set; } = string.Empty;
}