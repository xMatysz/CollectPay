using CollectPay.Application.Behaviors;
using CollectPay.Application.Common.Interactions;
using FluentValidation;
using MediatR;
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