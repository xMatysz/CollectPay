using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using CollectPay.Application.Common.Configuration;
using CollectPay.Application.Services;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace CollectPay.Infrastructure.Services;

//TODO: maybe it should not be a part of app
public class TokenService : ITokenService
{
	private readonly SecretProvider _secretProvider;

	public TokenService(IOptions<SecretProvider> secretProvider)
	{
		_secretProvider = secretProvider.Value;
	}

	public string GenerateToken(Guid userId)
	{
		var claims = new Claim[]
		{
			new(ClaimTypes.Sid, userId.ToString())
		};

		var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secretProvider.Jwt.TokenKey));

		var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha512Signature);

		var token = new JwtSecurityToken(
			_secretProvider.Jwt.Issuer,
			_secretProvider.Jwt.Audience,
			claims,
			null,
			DateTime.UtcNow.AddDays(1),
			credentials);

		return new JwtSecurityTokenHandler().WriteToken(token);
	}
}