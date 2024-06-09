using System.Collections.Immutable;
using System.Text.Json;
using CollectPay.Domain.BillAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
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

		// builder
		// 	.Property(b => b.Debtors)
		// 	.HasConversion(
		// 		from => JsonSerializer.Serialize(from, JsonSerializerOptions.Default),
		// 		to => string.IsNullOrEmpty(to)
		// 			? new List<Guid>()
		// 			: JsonSerializer.Deserialize<IReadOnlyCollection<Guid>>(to, JsonSerializerOptions.Default)!,
		// 		new ValueComparer<IReadOnlyCollection<Guid>>(
		// 			(c1, c2) => c1.SequenceEqual(c2),
		// 			c => c.Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode())),
		// 			c => c.ToList()));

		builder.HasMany(x => x.Debtors2)
			.WithOne()
			.HasForeignKey("BillId")
			.OnDelete(DeleteBehavior.Cascade);
	}
}

public class TestOvveride : IEntityTypeConfiguration<SpecialId>
{
	public void Configure(EntityTypeBuilder<SpecialId> builder)
	{
		builder.ToTable("SpecialIds");
		builder.HasKey(x => x.Id);
	}
}