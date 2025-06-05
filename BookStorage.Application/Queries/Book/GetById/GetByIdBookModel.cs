using BookStorage.Domain.Enums;

namespace BookStorage.Application.Queries.Book.GetById
{
	public class GetByIdBookModel
	{
		public Guid Id { get; set; }
		public string Title { get; set; } = null!;
		public BookGenre Genre { get; set; }
		public decimal Price { get; set; }
		public DateTime PublishDate { get; set; }
		public Guid AuthorId { get; set; }
	}
}
