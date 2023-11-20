using CollectPay.Application.Common.Repositories;
using CollectPay.Domain.BillAggregate;
using CollectPay.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CollectPay.Infrastructure.Repositories;

public sealed class BillRepository : IBillRepository
{
	private readonly CollectPayDbContext _dbContext;

	public BillRepository(CollectPayDbContext dbContext)
	{
		_dbContext = dbContext;
	}

	public async Task AddAsync(Bill bill, CancellationToken cancellationToken)
	{
		await _dbContext.AddAsync(bill, cancellationToken);
	}

	public async Task<List<Bill>> GetAllAsync(CancellationToken cancellationToken)
	{
		 return await _dbContext.Set<Bill>().ToListAsync(cancellationToken);
	}
}