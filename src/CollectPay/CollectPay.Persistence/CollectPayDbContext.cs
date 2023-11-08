using CollectPay.Persistence.Mappings;
using Microsoft.EntityFrameworkCore;

namespace CollectPay.Persistence;

public class CollectPayDbContext : DbContext
{
	public CollectPayDbContext(DbContextOptions<CollectPayDbContext> options)
		: base(options)
	{
	}

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		base.OnModelCreating(modelBuilder);

		modelBuilder.ApplyConfiguration(new BillConfigurationOverride());
		modelBuilder.ApplyConfiguration(new PaymentConfigurationOverride());
	}
}