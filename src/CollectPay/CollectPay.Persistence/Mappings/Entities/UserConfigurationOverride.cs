using CollectPay.Domain.UserAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CollectPay.Persistence.Mappings.Entities;

public class UserConfigurationOverride : IEntityTypeConfiguration<User>
{
	public void Configure(EntityTypeBuilder<User> builder)
	{
		builder.ToTable("Users");
		builder.HasKey(x => x.Id);

		builder.Property(x => x.Password)
			.HasColumnType("bytea")
			.HasConversion(
				to => (byte[])to,
				from => from);

		builder.Property(x => x.PasswordSalt)
			.HasColumnType("bytea")
			.HasConversion(
				to => (byte[])to,
				from => from);
	}
}