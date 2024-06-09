using CollectPay.Domain.BillAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CollectPay.Persistence.Mappings.Entities;

public class BillConfigurationOverride : IEntityTypeConfiguration<Bill>
{
	public void Configure(EntityTypeBuilder<Bill> builder)
	{
		builder.ToTable("Bills");
		builder.HasKey(x => x.Id);

		builder.Property(x => x.Id)
			.ValueGeneratedNever();

		builder.HasMany(x => x.Payments)
			.WithOne()
			.HasForeignKey(x=>x.BillId)
			.OnDelete(DeleteBehavior.Cascade);

		builder.PrimitiveCollection(x => x.Debtors);
	}
}