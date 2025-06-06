using BookStorage.Application.Queries.Book.GetAll;

namespace BookStorage.Application.Queries.Author.GetAll
{
	public class GetAllAuthorModel
	{
		public Guid Id { get; set; }
		public required string FirstName { get; set; }
		public required string LastName { get; set; }
		public DateTime BirthDate { get; set; }
		public List<GetAllBooksModel> Books { get; set; } = new();
	}
}
