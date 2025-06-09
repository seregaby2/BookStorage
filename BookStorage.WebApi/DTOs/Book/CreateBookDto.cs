using BookStorage.Domain.Enums;

namespace BookStorage.WebApi.DTOs.Book
{
	public class CreateBookDto
	{
		public required string Title { get; set; }
		public BookGenre Genre { get; set; }
		public decimal Price { get; set; }
		public Guid AuthorId { get; set; }
	}
}
