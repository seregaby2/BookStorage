using BookStorage.Infrastructure.Data;
using BookStorage.Infrastructure.Data.Repositories;
using BookStorage.Infrastructure.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace BookStorage.Infrastructure.Extensions
{
	public static class ServiceCollectionExtensions
	{
		public static IServiceCollection AddApplicationRepository(this IServiceCollection services)
		{
			services.AddScoped<IAuthorRepository, AuthorRepository>();
			services.AddScoped<IBookRepository, BookRepository>();
			services.AddScoped<ICustomerRepository, CustomerRepository>();
			services.AddScoped<IOrderRepository, OrderRepository>();
			services.AddScoped<IOrderBookRepository, OrderBookRepository>();
			services.AddScoped<IDbConnectionFactory, DbConnectionFactory>();

			return services;
		}
	}
}
