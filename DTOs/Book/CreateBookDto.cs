namespace BookStorage.DTOs.Book
{
    public class CreateBookDto
    {
        public string Title { get; set; } = null!;
        public string Genre { get; set; } = null!;
        public decimal Price { get; set; }
        public DateTime PublishDate { get; set; }
        public Guid AuthorId { get; set; }
    }
}
