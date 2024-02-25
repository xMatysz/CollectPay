using CollectPay.Application.Behaviors;
using FluentValidation;
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
			config.AddOpenBehavior(typeof(ValidationBehavior<,>));
			config.AddOpenBehavior(typeof(UnitOfWorkBehavior<,>));

			config.RegisterServicesFromAssembly(assembly);
		});

		services.AddValidatorsFromAssembly(assembly);

		return services;
	}
}