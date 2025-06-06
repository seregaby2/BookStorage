using BookStorage.Domain.Enums;

namespace BookStorage.Application.Commands.Book.Create
{
	public class CreateBookModel
	{
		public required string Title { get; set; }
		public BookGenre Genre { get; set; }
		public decimal Price { get; set; }
		public Guid AuthorId { get; set; }
	}
}
