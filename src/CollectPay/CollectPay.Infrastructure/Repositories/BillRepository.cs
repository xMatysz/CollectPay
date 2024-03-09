using CollectPay.Application.Common.Repositories;
using CollectPay.Domain.BillAggregate;
using CollectPay.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CollectPay.Infrastructure.Repositories;

public sealed class BillRepository : Repository, IBillRepository
{
	public BillRepository(CollectPayDbContext dbContext)
		: base(dbContext)
	{
	}

	public async Task AddAsync(Bill bill, CancellationToken cancellationToken)
	{
		await DbContext.AddAsync(bill, cancellationToken);
	}

	public async Task<Bill?> GetByIdAsync(Guid billId, CancellationToken cancellationToken)
	{
		return await DbContext.Set<Bill>()
			.Include(x => x.Payments)
			.FirstOrDefaultAsync(x => x.Id == billId, cancellationToken);
	}

	public async Task<List<Bill>> GetAllAsync(CancellationToken cancellationToken)
	{
		 return await DbContext.Set<Bill>().ToListAsync(cancellationToken);
	}

	public Task RemoveAsync(Bill bill, CancellationToken cancellationToken = default)
	{
		DbContext.Remove(bill);

		return Task.CompletedTask;
	}
}