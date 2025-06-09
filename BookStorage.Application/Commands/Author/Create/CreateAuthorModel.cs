namespace BookStorage.Application.Commands.Author.Create
{
	public class CreateAuthorModel
	{
		public required string FirstName { get; set; }
		public required string LastName { get; set; }
		public DateTime BirthDate { get; set; }
	}
}
