namespace BookStorage.WebApi.DTOs.Book
{
    public class UpdateBookDto
    {
        public string Title { get; set; } = null!;
        public string Genre { get; set; } = null!;
        public decimal Price { get; set; }
        public DateTime PublishDate { get; set; }
        public Guid AuthorId { get; set; }
    }
}
