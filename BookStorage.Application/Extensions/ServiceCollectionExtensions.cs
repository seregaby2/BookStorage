using BookStorage.Application.Implementations.Services;
using BookStorage.Application.Interfaces.Services;
using BookStorage.Application.Mediatr.OrderMediatr.CreateOrder;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace BookStorage.Application.Extensions
{
	public static class ServiceCollectionExtensions
	{
		public static IServiceCollection AddApplicationServices(this IServiceCollection services)
		{
			services.AddScoped<IAuthorService, AuthorService>();
			services.AddScoped<IBookService, BookService>();
			services.AddScoped<ICustomerService, CustomerService>();
			services.AddScoped<IOrderService, OrderService>();
			services.AddMediatR(typeof(CreateOrderCommand).Assembly);

			return services;
		}
	}
}
