using CollectPay.Domain.BillAggregate.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CollectPay.Persistence.Mappings.Entities;

public class PaymentConfigurationOverride : IEntityTypeConfiguration<Payment>
{
	public void Configure(EntityTypeBuilder<Payment> builder)
	{
		builder.ToTable("Payments");
		builder.HasKey(x => x.Id);

		builder.Property(x => x.Id)
			.ValueGeneratedNever();

		builder.OwnsOne(x => x.Amount, y =>
		{
			y.Property(c => c.Value)
				.HasColumnName("Amount")
				.IsRequired();

			y.Property(c => c.Currency)
				.HasColumnName("Currency")
				.HasMaxLength(3)
				.IsRequired();
		});
	}
}