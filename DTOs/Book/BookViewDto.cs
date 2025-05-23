namespace BookStorage.WebApi.DTOs.Book
{
    public class BookViewDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = null!;
        public string Genre { get; set; } = null!;
        public decimal Price { get; set; }
        public DateTime PublishDate { get; set; }
        public Guid AuthorId { get; set; }
        public string AuthorFullName { get; set; } = null!;
    }
}
