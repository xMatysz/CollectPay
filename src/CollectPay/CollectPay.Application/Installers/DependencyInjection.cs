using CollectPay.Application.Behaviors;
using Microsoft.Extensions.DependencyInjection;

namespace CollectPay.Application.Installers;

public static class DependencyInjection
{
	public static IServiceCollection AddApplication(
		this IServiceCollection services)
	{
		var assembly = typeof(DependencyInjection).Assembly;

		services.AddMediatR(config =>
		{
			config.RegisterServicesFromAssembly(assembly);

			config.AddOpenBehavior(typeof(ValidationBehavior<,>));
			config.AddOpenBehavior(typeof(UnitOfWorkBehavior2<,>));
		});

		return services;
	}
}