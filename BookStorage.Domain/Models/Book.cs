using System.ComponentModel.DataAnnotations;

namespace BookStorage.Domain.Models
{
    public class Book
    {
        [Key]
        public Guid Id { get; set; }
        public string Title { get; set; } = "unknown";
        public string Genre { get; set; } = "unknown";
        public DateTime PublishDate { get; set; }
        public decimal Price { get; set; }
        public Guid AuthrID { get; set; }
        public Author Author { get; set; }
    }
}
