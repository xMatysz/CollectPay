using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CollectPay.Persistence.Installers;

public static class DependencyInjection
{
	public static IServiceCollection AddPersistence(
		this IServiceCollection services,
		IConfiguration config)
	{
		var connectionString = config.GetSection("ConnectionString").Value;

		services.AddDbContext<CollectPayDbContext>(opt =>
			opt.UseNpgsql(connectionString));

		return services;
	}
}