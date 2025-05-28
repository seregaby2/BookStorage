using AutoMapper;
using BookStorage.Application.Interfaces.Services;
using BookStorage.Domain.Models;
using BookStorage.WebApi.DTOs.Book;
using Microsoft.AspNetCore.Mvc;

namespace BookStorage.WebApi.Controllers
{
	[ApiVersion("1.0")]
	[ApiController]
	[Route("api/v{version:apiVersion}/[controller]")]
	public class BookController : ControllerBase
	{
		private readonly IMapper _mapper;
		private readonly IBookService _bookService;

		public BookController(IMapper mapper, IBookService BookService)
		{
			_mapper = mapper;
			_bookService = BookService;
		}

		/// <summary>
		/// Retrieves all books available in the system
		/// </summary>
		/// <returns>A list of books</returns>
		/// <response code="200">Returns the list of books.</response>
		[HttpGet]
		[ProducesResponseType(typeof(IEnumerable<BookViewDto>), 200)]
		public ActionResult<IEnumerable<BookViewDto>> GetAll()
		{
			var books = _bookService.GetAll();

			var booksDto = _mapper.Map<List<BookViewDto>>(books);

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
			var book = _bookService.GetById(id);
			if (book == null)
				return NotFound();

			var bookDto = _mapper.Map<BookViewDto>(book);

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
		public ActionResult<BookViewDto> CreateAsync([FromBody] CreateBookDto bookDto)
		{
			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			var book = _mapper.Map<Book>(bookDto);

			var createdBook = _bookService.Create(book);
			if (createdBook == null)
				return NotFound($"Author was not found.");

			var createdDto = _mapper.Map<BookViewDto>(createdBook);

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
		public ActionResult<BookViewDto> UpdateAsync([FromRoute] Guid id, [FromBody] UpdateBookDto bookDto)
		{
			var bookToUpdate = _mapper.Map<Book>(bookDto);

			var updatedBook = _bookService.Update(id, bookToUpdate);
			if (updatedBook == null)
				return BadRequest($"Author or book were not found.");

			var bookViewDto = _mapper.Map<BookViewDto>(updatedBook);

			return Ok(bookViewDto);
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
			var bookToDelete = _bookService.Delete(id);
			if (bookToDelete)
				return NotFound();

			return NoContent();
		}
	}
}
