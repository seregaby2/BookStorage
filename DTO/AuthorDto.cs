namespace BookStorage.DTO
{
    public class AuthorDto
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
        public DateTime BirthDate { get; set; }
    }

    public class CreateAuthorDto
    {
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
        public DateTime BirthDate { get; set; }
    }

    public class UpdateAuthorDto
    {
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
        public DateTime BirthDate { get; set; }
    }
}
