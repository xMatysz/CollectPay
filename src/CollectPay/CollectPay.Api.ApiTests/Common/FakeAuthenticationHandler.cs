using System.Security.Claims;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace CollectPay.Api.ApiTests.Common;

public class FakeAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
{
	public const string FakeSchemaName = "TestScheme";

	public FakeAuthenticationHandler(
		IOptionsMonitor<AuthenticationSchemeOptions> options,
		ILoggerFactory logger,
		UrlEncoder encoder,
		ISystemClock clock)
		: base(options, logger, encoder, clock)
	{
	}

	public FakeAuthenticationHandler(
		IOptionsMonitor<AuthenticationSchemeOptions> options,
		ILoggerFactory logger,
		UrlEncoder encoder)
		: base(options, logger, encoder)
	{
	}

	protected override Task<AuthenticateResult> HandleAuthenticateAsync()
	{
		var identity = new ClaimsIdentity(Array.Empty<Claim>(), "Test");
		var principal = new ClaimsPrincipal(identity);
		var ticket = new AuthenticationTicket(principal, FakeSchemaName);

		var result = AuthenticateResult.Success(ticket);

		return Task.FromResult(result);
	}
}