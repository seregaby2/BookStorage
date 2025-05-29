using BookStorage.Application.Implementations.Services;
using BookStorage.Application.Interfaces.Services;
using BookStorage.Infrastructure.Data;
using BookStorage.Infrastructure.Data.Repositories;
using BookStorage.Infrastructure.Interfaces;

namespace BookStorage.WebApi.Extensions
{
	public static class ServiceCollectionExtensions
	{
		public static IServiceCollection AddApplicationServices(this IServiceCollection services)
		{
			services.AddScoped<IAuthorService, AuthorService>();
			services.AddScoped<IBookService, BookService>();
			services.AddScoped<ICustomerService, CustomerService>();
			services.AddScoped<IOrderService, OrderService>();
			services.AddScoped<IAuthorBookService, AuthorBookService>();
			services.AddScoped<IDbConnectionFactory, DbConnectionFactory>();
			services.AddScoped<IAuthorRepository, AuthorRepository>();

			return services;
		}
	}
}
