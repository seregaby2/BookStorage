using BookStorage.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookStorage.Controllers
{
    [ApiController]
    [Route("api/[controller]")]  
    public class BookController : ControllerBase
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

        /// <summary>
        /// Retrieves all books available in the system
        /// </summary>
        /// <returns>A list of books</returns>
        /// <response code="200">Returns the list of books.</response>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Book>), 200)]
        public ActionResult<IEnumerable<Book>> GetAll()
        {
            return Ok(_books);
        }


        /// <summary>
        /// Retrieves a specific book by their unique identifier
        /// </summary>
        /// <param name="id">The unique identifier of the book</param>
        /// <returns>The book with the specified ID</returns>
        /// <response code="200">Returns the book</response>
        /// <response code="404">If the book is not found</response>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Book), 200)]
        [ProducesResponseType(404)]
        public ActionResult<Book> GetById([FromRoute] Guid id)
        {
            var book = _books.FirstOrDefault(a => a.Id == id);
            if (book == null)
            {
                return NotFound();
            }
            return Ok(book);
        }

        /// <summary>
        /// Creates a new book
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /api/Book
        ///     {
        ///        "Title" : "War and Peace",
        ///        "Genre" : "Novel",
        ///        "Price" : "150",
        ///     }
        /// </remarks>
        /// <param name="book">The book to create</param>
        /// <returns>The newly created book</returns>
        /// <response code="201">Returns the newly created book</response>
        /// <response code="400">If the input model is invalid</response>
        [HttpPost]
        [ProducesResponseType(typeof(Book), 201)]
        [ProducesResponseType(400)]
        public ActionResult<Book> Create([FromBody] Book book)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _books.Add(book);
            return StatusCode(201, book);
        }

        /// <summary>
        /// Updates an existing book by their unique identifier
        /// </summary>
        /// <param name="id">The unique identifier of the book to update</param>
        /// <param name="book">The updated book data</param>
        /// <returns>The updated book</returns>
        /// <response code="200">Returns the updated book</response>
        /// <response code="404">If the book with the specified ID is not found</response>
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(Book), 200)]
        [ProducesResponseType(404)]
        public ActionResult<Book> Update([FromRoute] Guid id, [FromBody] Book book)
        {
            var existingBook = _books.FirstOrDefault(x => x.Id == id);
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

        /// <summary>
        /// Deletes an book by their unique identifier
        /// </summary>
        /// <param name="id">The unique identifier of the book to delete</param>
        /// <response code="204">Book was successfully deleted</response>
        /// <response code="404">Book with the specified ID was not found</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public ActionResult Delete([FromRoute] Guid id)
        {
            var existingBook = _books.FirstOrDefault(x => x.Id == id);
            if (existingBook == null)
            {
                return NotFound();
            }
            _books.Remove(existingBook);
            return NoContent();
        }
    }
}
