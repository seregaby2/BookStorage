namespace BookStorage.DTO
{
    public class BookDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = null!;
        public string Genre { get; set; } = null!;
        public decimal Price { get; set; }
        public DateTime PublishDate { get; set; }
        public Guid AuthorId { get; set; }
        public string AuthorFullName { get; set; } = null!;
    }

    public class CreateBookDto
    {
        public string Title { get; set; } = null!;
        public string Genre { get; set; } = null!;
        public decimal Price { get; set; }
        public DateTime PublishDate { get; set; }
        public Guid AuthorId { get; set; }
    }

    public class UpdateBookDto
    {
        public string Title { get; set; } = null!;
        public string Genre { get; set; } = null!;
        public decimal Price { get; set; }
        public DateTime PublishDate { get; set; }
        public Guid AuthorId { get; set; }
    }
}
