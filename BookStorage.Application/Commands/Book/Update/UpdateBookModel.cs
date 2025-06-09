using BookStorage.Domain.Enums;

namespace BookStorage.Application.Commands.Book.Update
{
	public class UpdateBookModel
	{
		public required string Title { get; set; }
		public BookGenre Genre { get; set; }
		public decimal Price { get; set; }
		public Guid AuthorId { get; set; }
	}
}
