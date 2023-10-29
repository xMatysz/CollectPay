#pragma warning disable IDE0005
using CollectPay.Application.Common.Repositories;
using Microsoft.EntityFrameworkCore;

#pragma warning restore IDE0005

namespace CollectPay.Infrastructure.Persistence.Repositories;

public abstract class Repository<T, TId> : IRepository<T, TId>
	where T : class
{
	private readonly CollectPayDbContext _dbContext;

	protected Repository(CollectPayDbContext dbContext)
	{
		_dbContext = dbContext;
	}

	public Task<T[]> GetAll()
	{
		return _dbContext.Set<T>().ToArrayAsync();
	}

	public T? GetById(TId id)
	{
		return _dbContext.Set<T>().Find(id);
	}

	public void Add(T item)
	{
		_dbContext.Add(item);
	}
}