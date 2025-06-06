namespace BookStorage.Application.Commands.Author.Update
{
	public class UpdateAuthorModel
	{
		public required string FirstName { get; set; }
		public required string LastName { get; set; }
		public DateTime BirthDate { get; set; }
	}
}
