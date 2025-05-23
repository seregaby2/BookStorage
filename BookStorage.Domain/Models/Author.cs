using System.ComponentModel.DataAnnotations;

namespace BookStorage.Domain.Models
{
    public class Author
    {
        [Key]
        public Guid Id { get; set; }
        public string FirstName { get; set; } = "unknown";
        public string LastName { get; set; } = "unknown";
        public DateTime BirthDate { get; set; }
        public List<Book> Books { get; set; }
    }
}
