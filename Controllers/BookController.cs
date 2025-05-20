using BookStorage.DTOs.Book;
using BookStorage.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookStorage.Controllers
{
    [ApiController]
    [Route("api/[controller]")]  
    public class BookController : ControllerBase
    {
        private static readonly List<Author> _authors = AuthorController._authors;
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
        [ProducesResponseType(typeof(IEnumerable<BookViewDto>), 200)]
        public ActionResult<IEnumerable<BookViewDto>> GetAll()
        {
            var booksDto = _books.Select(a => new BookViewDto
            {
                Id = a.Id,
                Title = a.Title,
                Genre = a.Genre,
                Price = a.Price,
                PublishDate = a.PublishDate,
                AuthorId = a.AuthrID,
                AuthorFullName = GetAuthorFullName(a.AuthrID)
            });

            return Ok(booksDto);
        }

        /// <summary>
        /// Retrieves a specific book by their unique identifier
        /// </summary>
        /// <param name="id">The unique identifier of the book</param>
        /// <returns>The book with the specified ID</returns>
        /// <response code="200">Returns the book</response>
        /// <response code="404">If the book is not found</response>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(BookViewDto), 200)]
        [ProducesResponseType(404)]
        public ActionResult<BookViewDto> GetById([FromRoute] Guid id)
        {
            var book = _books.FirstOrDefault(a => a.Id == id);
            if (book == null)
            {
                return NotFound();
            }

            var bookDto = new BookViewDto
            {
                Id = book.Id,
                Title = book.Title,
                Genre = book.Genre,
                Price = book.Price,
                PublishDate = book.PublishDate,
                AuthorId = book.AuthrID,
                AuthorFullName = GetAuthorFullName(book.AuthrID)
            };
            
            return Ok(bookDto);
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
        /// <param name="bookDto">The book to create</param>
        /// <returns>The newly created book</returns>
        /// <response code="201">Returns the newly created book</response>
        /// <response code="400">If the input model is invalid</response>
        [HttpPost]
        [ProducesResponseType(typeof(BookViewDto), 201)]
        [ProducesResponseType(400)]
        public ActionResult<BookViewDto> Create([FromBody] CreateBookDto bookDto)
        {
            var author = _authors.FirstOrDefault(a => a.Id == bookDto.AuthorId);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (author == null)
            {
                return BadRequest($"Author with ID {bookDto.AuthorId} was not found.");
            }
                
            var book = new Book
            {
                Id = Guid.NewGuid(),
                Title = bookDto.Title,
                Genre = bookDto.Genre,
                Price = bookDto.Price,
                PublishDate = bookDto.PublishDate,
                AuthrID = bookDto.AuthorId,
                Author = author
            };

            _books.Add(book);

            var createdDto = new BookViewDto
            {
                Id = book.Id,
                Title = book.Title,
                Genre = book.Genre,
                Price = book.Price,
                PublishDate = book.PublishDate,
                AuthorId = book.AuthrID,
                AuthorFullName = $"{author.FirstName} {author.LastName}"
            };

            return StatusCode(201, createdDto);
        }

        /// <summary>
        /// Updates an existing book by their unique identifier
        /// </summary>
        /// <param name="id">The unique identifier of the book to update</param>
        /// <param name="bookDto">The updated book data</param>
        /// <returns>The updated book</returns>
        /// <response code="200">Returns the updated book</response>
        /// <response code="404">If the book with the specified ID is not found</response>
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(BookViewDto), 200)]
        [ProducesResponseType(404)]
        public ActionResult<BookViewDto> Update([FromRoute] Guid id, [FromBody] UpdateBookDto bookDto)
        {
            var existingBook = _books.FirstOrDefault(x => x.Id == id);
            if (existingBook == null)
            {
                return NotFound();
            }

            var author = _authors.FirstOrDefault(a => a.Id == bookDto.AuthorId);

            if (author == null)
            {
                return BadRequest($"Author with ID {bookDto.AuthorId} was not found.");
            }

            existingBook.Title = bookDto.Title;
            existingBook.Genre = bookDto.Genre;
            existingBook.Price = bookDto.Price;
            existingBook.PublishDate = bookDto.PublishDate;
            existingBook.AuthrID = bookDto.AuthorId;
            existingBook.Author = author;
            
            var UpdateDdto = new BookViewDto
            {
                Id = existingBook.Id,
                Title = existingBook.Title,
                Genre = existingBook.Genre,
                Price = existingBook.Price,
                PublishDate = existingBook.PublishDate,
                AuthorId = existingBook.AuthrID,
                AuthorFullName = $"{author.FirstName} {author.LastName}"
       
            };

            return Ok(UpdateDdto);
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

        private static string GetAuthorFullName(Guid authorId)
        {
            var author = _authors.FirstOrDefault(a => a.Id == authorId);

            return author == null ? "Unknown" : $"{author.FirstName} {author.LastName}";
        }
    }
}
