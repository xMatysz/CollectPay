﻿using Microsoft.EntityFrameworkCore;

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

		modelBuilder.ApplyConfigurationsFromAssembly(typeof(CollectPayDbContext).Assembly);
	}
}