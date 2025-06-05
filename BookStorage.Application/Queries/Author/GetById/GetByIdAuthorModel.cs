using BookStorage.Domain.Enums;

namespace BookStorage.Application.Queries.Author.GetById
{
	public class GetByIdAuthorModel
	{
		public Guid Id { get; set; }
		public string Title { get; set; } = null!;
		public BookGenre Genre { get; set; }
		public decimal Price { get; set; }
		public DateTime PublishDate { get; set; }
		public Guid AuthorId { get; set; }
	}
}
