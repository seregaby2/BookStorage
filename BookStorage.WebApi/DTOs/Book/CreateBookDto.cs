using BookStorage.Domain.Enums;

namespace BookStorage.WebApi.DTOs.Book
{
    public class CreateBookDto
    {
        public string Title { get; set; } = null!;
        public BookGenre Genre { get; set; }
        public decimal Price { get; set; }
    }
}
