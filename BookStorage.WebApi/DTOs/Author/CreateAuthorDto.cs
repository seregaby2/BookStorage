namespace BookStorage.WebApi.DTOs.Author
{
	public class CreateAuthorDto
	{
		public required string FirstName { get; set; }
		public required string LastName { get; set; }
		public DateTime BirthDate { get; set; }
	}
}
