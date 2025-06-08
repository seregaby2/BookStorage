using BookStorage.Application.Implementations.Services;
using BookStorage.Domain.Models;
using BookStorage.Infrastructure.Interfaces;
using Moq;

public class OrderServiceTests
{
	[Fact]
	public async Task CountTotalAmount_ShouldCalculateCorrectTotal()
	{
		var book1 = new Book { Id = Guid.NewGuid(), Price = 10, Author = new Author { Id = Guid.NewGuid(), FirstName = "FirstName 1", LastName = "LastName 1" } };
		var book2 = new Book { Id = Guid.NewGuid(), Price = 20, Author = new Author { Id = Guid.NewGuid(), FirstName = "FirstName 2", LastName = "LastName 1" } };

		var order = new Order
		{
			Customer = new Customer { Id = Guid.NewGuid(), FirstName = "Test Customer", Email = "test@example.com", PhoneNumber = "123456789" },
			OrderBooks = new List<OrderBook>
			{
				new OrderBook { BookId = book1.Id, Quantity = 2 },
				new OrderBook { BookId = book2.Id, Quantity = 1 }
			}
		};

		var bookRepositoryMock = new Mock<IBookRepository>();
		bookRepositoryMock.Setup(repo => repo.GetByIdAsync(book1.Id))
			.ReturnsAsync(book1);
		bookRepositoryMock.Setup(repo => repo.GetByIdAsync(book2.Id))
			.ReturnsAsync(book2);

		var service = new OrderService(bookRepositoryMock.Object);

		await service.CountTotalAmount(order);

		Assert.Equal(40, order.TotalAmount);
		Assert.Equal(book1, order.OrderBooks[0].Book);
		Assert.Equal(book2, order.OrderBooks[1].Book);

		bookRepositoryMock.Verify(repo => repo.GetByIdAsync(book1.Id), Times.Once);
		bookRepositoryMock.Verify(repo => repo.GetByIdAsync(book2.Id), Times.Once);
	}
}
