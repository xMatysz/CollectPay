using System.Text;
using CollectPay.Application.Common.Configuration;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace CollectPay.Api.Authentication;

public class JwtBearerOptionsSetup : IConfigureNamedOptions<JwtBearerOptions>
{
	private readonly SecretProvider _secretProvider;

	public JwtBearerOptionsSetup(IOptions<SecretProvider> secretProvider)
	{
		_secretProvider = secretProvider.Value;
	}
	public void Configure(JwtBearerOptions options)
	{
		options.TokenValidationParameters = new()
			{
				ValidateIssuerSigningKey = true,
				IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secretProvider.Jwt.TokenKey)),
				ValidateIssuer = true,
				ValidIssuer = _secretProvider.Jwt.Issuer,
				ValidateAudience = true,
				ValidAudience = _secretProvider.Jwt.Audience,
				ValidateLifetime = true
			};
	}

	public void Configure(string name, JwtBearerOptions options)
	{
		Configure(options);
	}
}