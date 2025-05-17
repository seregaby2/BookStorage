using BookStorage.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookStorage.Controllers
{
    [ApiController]
    [Route("api/[controller]")]  
    public class BooksController : ControllerBase
    {
        private static readonly List<Book> _books = new List<Book>
        {
            new Book
            {
                Id = Guid.NewGuid(),
                Title = "White Fang",
                Genre = "Adventure",
                Price = 100,
                PublishDate = new DateTime(1906,10,1),
                Author = new Author(),
                AuthrID = Guid.NewGuid()
            },
            new Book
            {
                Id = Guid.NewGuid(),
                Title = "War and Peace",
                Genre = "Novel",
                Price = 150,
                PublishDate = new DateTime(1869,12,1),
                Author = new Author(),
                AuthrID = Guid.NewGuid()
            }
        };

        [HttpGet]
        public ActionResult<IEnumerable<Book>> GetAll()
        {
            return Ok(_books);
        }

        [HttpGet("{id}")]
        public ActionResult<Book> GetById([FromRoute] Guid id)
        {
            var book = _books.FirstOrDefault(x => x.Id == id);
            if (book == null)
            {
                return NotFound();
            }
            return Ok(book);
        }

        [HttpPost]
        public ActionResult<Book> Create([FromBody] Book book)
        {
            _books.Add(book);
            return Ok(book);
        }

        [HttpPut("{id}")]
        public ActionResult<Book> Update([FromRoute] Guid id,  [FromBody] Book book)
        {
            var existingBook = _books.FirstOrDefault(x=>x.Id == id);
            if (existingBook == null)
            {
                return NotFound();
            }
            existingBook.Title = book.Title;
            existingBook.PublishDate = book.PublishDate;
            existingBook.Price = book.Price;
            existingBook.Author = book.Author;
            return Ok(book);
        }

        [HttpDelete("{id}")] 
        public ActionResult<Book> Delete([FromRoute] Guid id)
        {
            var existingBook = _books.FirstOrDefault(x => x.Id == id);
            if (existingBook == null)
            {
                return NotFound();
            }
            _books.Remove(existingBook);
            return Ok(existingBook);
        }

    }
}
