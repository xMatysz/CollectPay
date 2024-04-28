﻿using CollectPay.Application.Common.Repositories;
using CollectPay.Domain.Common.Models;
using CollectPay.Persistence;
using CollectPay.Tests.Integration.Shared;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CollectPay.Application.Tests.Integration;

public abstract class IntegrationTestBase : IClassFixture<WebApiFactory>, IAsyncLifetime
{
	private readonly Func<Task> _resetDb;
	private readonly WebApiFactory _factory;
	private CollectPayDbContext _dbContext;
	private IServiceScope _scope;

	private IServiceProvider ServiceProvider { get; set; }

	protected ISender Sender { get; set; }
	protected IBillRepository BillRepository { get; set; }
	protected IUnitOfWork UnitOfWork { get; set; }
	protected IUserRepository UserRepository { get; set; }

	protected IntegrationTestBase(WebApiFactory factory)
	{
		_factory = factory;
		_resetDb = _factory.ResetDbAsync;
		RestartScope();
	}

	public Task InitializeAsync() => Task.CompletedTask;

	public async Task DisposeAsync() => await _resetDb();

	protected void AssumeEntityInDb<TEntity>(params TEntity[] entities)
		where TEntity : Entity
	{
		foreach (var entity in entities)
		{
			_dbContext.Add(entity);
		}

		UnitOfWork.SaveChangesAsync().GetAwaiter().GetResult();
		RestartScope();
	}

	protected async Task AssumeEntityInDbAsync<TEntity>(params TEntity[] entities)
		where TEntity : Entity
	{
		foreach (var entity in entities)
		{
			await _dbContext.AddAsync(entity);
		}

		await UnitOfWork.SaveChangesAsync();
		RestartScope();
	}

	protected Task<TEntity[]> GetAllFromDb<TEntity>()
		where TEntity : Entity
	{
		return _dbContext.Set<TEntity>().ToArrayAsync();
	}

	private void RestartScope()
	{
		_scope = _factory.Services.CreateScope();
		ServiceProvider = _scope.ServiceProvider;

		_dbContext = ServiceProvider.GetRequiredService<CollectPayDbContext>();

		Sender = ServiceProvider.GetRequiredService<ISender>();

		BillRepository = ServiceProvider.GetRequiredService<IBillRepository>();
		UnitOfWork = ServiceProvider.GetRequiredService<IUnitOfWork>();
		UserRepository = ServiceProvider.GetRequiredService<IUserRepository>();
	}
}