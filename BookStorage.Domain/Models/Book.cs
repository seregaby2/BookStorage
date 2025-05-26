using System.ComponentModel.DataAnnotations;
using BookStorage.Domain.Enums;

namespace BookStorage.Domain.Models
{
    public class Book
    {
        [Key]
        public Guid Id { get; set; }
        public string Title { get; set; } = "unknown";
        public BookGenre Genre { get; set; } = BookGenre.Unknown;
        public DateTime PublishDate { get; set; }
        public decimal Price { get; set; }
        public Guid AuthrID { get; set; }
        public Author Author { get; set; }
    }
}
