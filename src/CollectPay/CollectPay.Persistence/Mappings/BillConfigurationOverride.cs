using CollectPay.Domain.BillAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CollectPay.Persistence.Mappings;

public class BillConfigurationOverride : IEntityTypeConfiguration<Bill>
{
	public void Configure(EntityTypeBuilder<Bill> builder)
	{
		builder.ToTable("Bills");
		builder.HasKey(x => x.Id);

		builder.HasMany(x => x.Payments).WithOne().HasForeignKey("BillId").OnDelete(DeleteBehavior.Cascade);
	}
}