using CollectPay.Application.Common.Configuration;
using CollectPay.Infrastructure.Services;
using FluentAssertions;
using Microsoft.Extensions.Options;
using NSubstitute;
using Xunit;

namespace CollectPay.Infrastructure.Tests.Unit.ServicesTests;

public class WhenUsingTokenService
{
	[Fact]
	public void Should_GenerateValidToken()
	{
		var userId = Guid.NewGuid();
		const string longTokenKey = "my top secret key which has to be long and long even more long and one more";
		const string audience = nameof(audience);
		const string issuer = nameof(issuer);

		var provider = new SecretProvider
		{
			Jwt = new JwtOptions
			{
				TokenKey = longTokenKey,
				Audience = audience,
				Issuer = issuer
			}
		};

		var options = Substitute.For<IOptions<SecretProvider>>();
		options.Value.Returns(provider);
		var sut = new TokenService(options);

		var result = sut.GenerateToken(userId);

		result.Should().NotBeNullOrEmpty();
	}
}