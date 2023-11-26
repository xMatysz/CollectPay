using CollectPay.Application.Common.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace CollectPay.Application.IntegrationTests;

public abstract class IntegrationTestBase : IAsyncLifetime
{
	private readonly Func<Task> _resetDb;
	protected static BillBuilder BillBuilder => new();
	protected static PaymentBuilder PaymentBuilder => new();
	protected IBillRepository BillRepository { get; }
	private IServiceProvider ServiceProvider { get; }
	protected IUnitOfWork UnitOfWork { get; }

	protected IntegrationTestBase(WebApiFactory factory)
	{
		_resetDb = factory.ResetDbAsync;

		var scope = factory.Services.CreateScope();
		ServiceProvider = scope.ServiceProvider;
		BillRepository = ServiceProvider.GetRequiredService<IBillRepository>();
		UnitOfWork = ServiceProvider.GetRequiredService<IUnitOfWork>();
	}

	public Task InitializeAsync() => Task.CompletedTask;

	public async Task DisposeAsync() => await _resetDb();
}