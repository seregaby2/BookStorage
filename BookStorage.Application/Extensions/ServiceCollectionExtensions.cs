using BookStorage.Application.Behaviors;
using BookStorage.Application.Commands.Customer.Create;
using BookStorage.Application.Implementations.Services;
using BookStorage.Application.Interfaces.Services;
using BookStorage.Application.MappingProfiles;
using BookStorage.Application.Mappings;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace BookStorage.Application.Extensions
{
	public static class ServiceCollectionExtensions
	{
		public static IServiceCollection AddApplicationServices(this IServiceCollection services)
		{
			services.AddScoped<IOrderService, OrderService>();
			services.AddScoped<IUserService, UserService>();

			services.AddMediatR(typeof(CreateCustomerCommand).Assembly);
			services.AddValidatorsFromAssembly(typeof(ServiceCollectionExtensions).Assembly);
			services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

			services.AddAutoMapper(typeof(OrderProfile));
			services.AddAutoMapper(typeof(CustomerProfile));
			services.AddAutoMapper(typeof(BookProfile));
			services.AddAutoMapper(typeof(AuthorProfile));

			return services;
		}
	}
}
