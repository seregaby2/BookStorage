using BookStorage.Domain.Enums;

namespace BookStorage.WebApi.DTOs.Book
{
    public class BookViewDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = null!;
        public BookGenre Genre { get; set; }
        public decimal Price { get; set; }
        public DateTime PublishDate { get; set; }
        public Guid AuthorId { get; set; }
        public string AuthorFullName { get; set; } = null!;
    }
}
