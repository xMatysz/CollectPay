using CollectPay.Application.BillAggregate.Queries;
using CollectPay.Domain.BillAggregate;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

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
			_configure(services);
		});
	}
}