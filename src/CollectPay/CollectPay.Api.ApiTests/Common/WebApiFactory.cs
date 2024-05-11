using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;

namespace CollectPay.Api.ApiTests.Common;

public sealed class WebApiFactory : WebApplicationFactory<IApiAssemblyMarker>
{
	private readonly Action<IServiceCollection> _configure;

	public WebApiFactory(Action<IServiceCollection> configure)
	{
		_configure = configure;
	}

	protected override void ConfigureWebHost(IWebHostBuilder builder)
	{
		base.ConfigureWebHost(builder);

		builder.UseEnvironment("Development");

		builder.ConfigureTestServices(services =>
		{
			services.RemoveAll(typeof(IPipelineBehavior<,>));
			services.RemoveAll(typeof(IConfigureNamedOptions<JwtBearerOptions>));

			services.AddAuthentication(cfg =>
				{
					cfg.DefaultScheme = FakeAuthenticationHandler.FakeSchemaName;
					cfg.DefaultAuthenticateScheme = FakeAuthenticationHandler.FakeSchemaName;
					cfg.DefaultChallengeScheme = FakeAuthenticationHandler.FakeSchemaName;
				})
				.AddScheme<AuthenticationSchemeOptions, FakeAuthenticationHandler>(
					FakeAuthenticationHandler.FakeSchemaName, _ => { });

			_configure(services);
		});
	}
}