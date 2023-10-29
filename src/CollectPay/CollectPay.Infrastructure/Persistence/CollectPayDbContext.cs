using Microsoft.EntityFrameworkCore;

namespace CollectPay.Infrastructure.Persistence;

public class CollectPayDbContext : DbContext
{
	public CollectPayDbContext(DbContextOptions<CollectPayDbContext> options)
		: base(options)
	{
	}
}