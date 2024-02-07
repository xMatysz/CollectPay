using CollectPay.Application.Common.Repositories;
using CollectPay.Application.Services;
using CollectPay.Domain.Common.Models;
using CollectPay.Persistence;
using Microsoft.Extensions.DependencyInjection;

namespace CollectPay.Application.IntegrationTests;

public abstract class IntegrationTestBase : IAsyncLifetime
{
	private readonly Func<Task> _resetDb;
	protected static BillBuilder BillBuilder => new();
	protected static PaymentBuilder PaymentBuilder => new();
	private CollectPayDbContext DbContext { get; set; }
	protected IBillRepository BillRepository { get; }
	protected IUserRepository UserRepository { get; }
	private IServiceProvider ServiceProvider { get; }
	protected IUnitOfWork UnitOfWork { get; }
	protected IHashPasswordService HashPasswordService { get; }

	protected IntegrationTestBase(WebApiFactory factory)
	{
		_resetDb = factory.ResetDbAsync;

		var scope = factory.Services.CreateScope();
		ServiceProvider = scope.ServiceProvider;

		DbContext = ServiceProvider.GetRequiredService<CollectPayDbContext>();
		UnitOfWork = ServiceProvider.GetRequiredService<IUnitOfWork>();
		BillRepository = ServiceProvider.GetRequiredService<IBillRepository>();
		UserRepository = ServiceProvider.GetRequiredService<IUserRepository>();
		HashPasswordService = ServiceProvider.GetRequiredService<IHashPasswordService>();
	}


	protected async Task AddEntityToDbAsync<T>(params T[] entries)
		where T : Entity
	{
		await DbContext.AddRangeAsync(entries);
		await UnitOfWork.SaveChangesAsync();
	}

	public Task InitializeAsync() => Task.CompletedTask;

	public async Task DisposeAsync() => await _resetDb();
}