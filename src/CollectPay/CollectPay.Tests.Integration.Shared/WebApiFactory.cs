using System.Data.Common;
using CollectPay.Api;
using CollectPay.Application.Common.Configuration;
using CollectPay.Persistence;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Npgsql;
using Respawn;
using Respawn.Graph;
using Testcontainers.PostgreSql;
using Xunit;

namespace CollectPay.Tests.Integration.Shared;

public sealed class WebApiFactory : WebApplicationFactory<IApiAssemblyMarker>, IAsyncLifetime
{
	private readonly PostgreSqlContainer _dbContainer =
		new PostgreSqlBuilder()
			.Build();
	private DbConnection _dbConnection = default!;
	private Respawner _respawner = default!;

	protected override void ConfigureWebHost(IWebHostBuilder builder)
	{
		base.ConfigureWebHost(builder);

		builder.ConfigureTestServices(services =>
		{
			services.RemoveAll(typeof(DbContextOptions<CollectPayDbContext>));
			services.AddDbContext<CollectPayDbContext>(options =>
				options.UseNpgsql(_dbContainer.GetConnectionString() + ";Include Error Detail=true;"));

			services.Configure<SecretProvider>(provider =>
			{
				provider.Jwt = new JwtOptions
				{
					TokenKey = "TestTestTestTestTestTestTestTestTestTestTestTestTestTestTestTestTestTestTestTest"
				};
			});
		});
	}

	public async Task ResetDbAsync() =>
		await _respawner.ResetAsync(_dbConnection);

	public async Task InitializeAsync()
	{
		await _dbContainer.StartAsync();

		await MigrateDb();
		await InitializeRespawner();
	}

	private async Task MigrateDb()
	{
		using var scope = Services.CreateScope();
		var dbContext = scope.ServiceProvider.GetRequiredService<CollectPayDbContext>();
		await dbContext.Database.MigrateAsync();
	}

	private async Task InitializeRespawner()
	{
		_dbConnection = new NpgsqlConnection(_dbContainer.GetConnectionString());
		await _dbConnection.OpenAsync();

		_respawner = await Respawner.CreateAsync(_dbConnection,
			new RespawnerOptions
			{
				DbAdapter = DbAdapter.Postgres,
				SchemasToInclude = ["public"],
				TablesToIgnore = [new Table("__EFMigrationsHistory")]
			});
	}

	public new async Task DisposeAsync() =>
		await _dbContainer.DisposeAsync().AsTask();
}