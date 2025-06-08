using NetArchTest.Rules;

namespace BookStorage.ArchitectureTests
{
	public class ArchitectureTests
	{
		[Fact]
		public void Domain_Should_Not_HaveDependencyOn_Application()
		{
			var result = Types
				.InAssembly(typeof(BookStorage.Domain.Models.Customer).Assembly)
				.ShouldNot()
				.HaveDependencyOn("BookStorage.Application")
				.GetResult();

			Assert.True(result.IsSuccessful, "Domain layer should not depend on Application layer.");
		}

		[Fact]
		public void Domain_Should_Not_HaveDependencyOn_Infrastructure()
		{
			var result = Types
				.InAssembly(typeof(BookStorage.Domain.Models.Customer).Assembly)
				.ShouldNot()
				.HaveDependencyOn("BookStorage.Infrastructure")
				.GetResult();

			Assert.True(result.IsSuccessful, "Domain layer should not depend on Infrastructure layer.");
		}

		[Fact]
		public void Application_Should_Not_HaveDependencyOn_WebApi()
		{
			var result = Types
				.InAssembly(typeof(BookStorage.Application.Commands.Customer.Create.CreateCustomerCommand).Assembly)
				.ShouldNot()
				.HaveDependencyOn("BookStorage.WebApi")
				.GetResult();

			Assert.True(result.IsSuccessful, "Application layer should not depend on WebApi layer.");
		}

		[Fact]
		public void Infrastructure_Should_Not_HaveDependencyOn_WebApi()
		{
			var result = Types
				.InAssembly(typeof(BookStorage.Infrastructure.Data.Repositories.AuthorRepository).Assembly)
				.ShouldNot()
				.HaveDependencyOn("BookStorage.WebApi")
				.GetResult();

			Assert.True(result.IsSuccessful, "Infrastructure layer should not depend on WebApi layer.");
		}
	}
}
