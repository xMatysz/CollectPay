using CollectPay.Application.Common.Repositories;
using CollectPay.Domain.Common.Models;
using CollectPay.Persistence;
using Microsoft.Extensions.DependencyInjection;

namespace CollectPay.Application.IntegrationTests;

public abstract class IntegrationTestBase : IAsyncLifetime
{
	private readonly Func<Task> _resetDb;
	private readonly CollectPayDbContext _dbContext;

	protected static BillBuilder BillBuilder => new();
	protected static PaymentBuilder PaymentBuilder => new();
	protected IBillRepository BillRepository { get; }
	protected IUserRepository UserRepository { get; }
	protected IUnitOfWork UnitOfWork { get; }

	protected IntegrationTestBase(WebApiFactory factory)
	{
		_resetDb = factory.ResetDbAsync;

		var scope = factory.Services.CreateScope();
		var serviceProvider = scope.ServiceProvider;

		_dbContext = serviceProvider.GetRequiredService<CollectPayDbContext>();
		UnitOfWork = serviceProvider.GetRequiredService<IUnitOfWork>();
		BillRepository = serviceProvider.GetRequiredService<IBillRepository>();
		UserRepository = serviceProvider.GetRequiredService<IUserRepository>();
	}


	protected async Task AddEntityToDbAsync<T>(params T[] entries)
		where T : Entity
	{
		await _dbContext.AddRangeAsync(entries);
		await UnitOfWork.SaveChangesAsync();
	}

	public Task InitializeAsync() => Task.CompletedTask;

	public async Task DisposeAsync() => await _resetDb();
}