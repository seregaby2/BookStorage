namespace BookStorage.WebApi.DTOs.Author
{
    public class AuthorViewDto
    {
        public Guid Id { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public DateTime BirthDate { get; set; }
    }
}
