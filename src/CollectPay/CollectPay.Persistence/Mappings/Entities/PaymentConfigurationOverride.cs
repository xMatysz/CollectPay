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

		builder.PrimitiveCollection(x => x.Debtors);

		var amountBuilder = builder.ComplexProperty(x => x.Amount);

		amountBuilder.Property(x => x.Value)
			.HasColumnName("Amount")
			.HasPrecision(18, 2)
			.IsRequired();

		amountBuilder.Property(x=>x.Currency)
			.HasColumnName("Currency")
			.HasMaxLength(3)
			.IsRequired();
	}
}