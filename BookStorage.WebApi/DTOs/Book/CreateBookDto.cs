using BookStorage.Domain.Enums;

namespace BookStorage.WebApi.DTOs.Book
{
	public class CreateBookDto
	{
		public required string Title { get; set; } = null!;
		public BookGenre Genre { get; set; }
		public required decimal Price { get; set; }
		public required Guid AuthorId { get; set; }
	}
}
