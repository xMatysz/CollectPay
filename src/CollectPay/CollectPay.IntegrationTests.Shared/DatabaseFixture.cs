using CollectPay.Api;
using CollectPay.Persistence;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Testcontainers.PostgreSql;
using Xunit;

namespace CollectPay.IntegrationTests.Shared;

public class ApiFactory : SomeFactory, IAsyncLifetime
{
	private readonly PostgreSqlContainer _dbContainer =
		new PostgreSqlBuilder()
			.Build();

	public async Task InitializeAsync()
	{
		await _dbContainer.StartAsync();
	}

	public new async Task DisposeAsync()
	{
		await _dbContainer.DisposeAsync().AsTask();
	}
}

public class SomeFactory : WebApplicationFactory<IApiAssemblyMarker>
{
	protected override void ConfigureWebHost(IWebHostBuilder builder)
	{
		builder.UseEnvironment("Development");

		builder.ConfigureTestServices(services =>
		{
			var descriptor = services.FirstOrDefault(x => x.ServiceType == typeof(CollectPayDbContext));
			if ( descriptor is not null )
			{
				services.Remove(descriptor);
			}

			// services.AddDbContext<CollectPayDbContext>(opt =>
			// 	opt.UseNpgsql(_dbContainer.GetConnectionString()));
			//
			// services.AddDbContext<CollectPayReadOnlyContext>(opt =>
			// 	opt.UseNpgsql(_dbContainer.GetConnectionString()));
		});
	}
}