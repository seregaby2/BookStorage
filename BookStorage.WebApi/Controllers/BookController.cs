using AutoMapper;
using BookStorage.Application.Commands.Book.Create;
using BookStorage.Application.Commands.Book.Delete;
using BookStorage.Application.Commands.Book.Update;
using BookStorage.Application.Interfaces.Services;
using BookStorage.Application.Queries.Book.GetAll;
using BookStorage.Application.Queries.Book.GetById;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BookStorage.WebApi.Controllers
{
	[ApiVersion("1.0")]
	[ApiController]
	[Route("api/v{version:apiVersion}/[controller]")]
	public class BookController : ControllerBase
	{
		private readonly IMapper _mapper;
		private readonly IMediator _mediator;
		private readonly IBookService _bookService;

		public BookController(IMapper mapper, IMediator mediator, IBookService BookService)
		{
			_mapper = mapper;
			_mediator = mediator;
			_bookService = BookService;
		}

		/// <summary>
		/// Retrieves all books available in the system
		/// </summary>
		/// <returns>A list of books</returns>
		/// <response code="200">Returns the list of books.</response>
		[HttpGet]
		[ProducesResponseType(typeof(IEnumerable<GetAllBooksModel>), 200)]
		public async Task<ActionResult<IEnumerable<GetAllBooksModel>>> GetAll()
		{
			var books = await _mediator.Send(new GetAllBooksQuery());

			var booksDto = _mapper.Map<List<GetAllBooksModel>>(books);

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
		[ProducesResponseType(typeof(GetByIdBookModel), 200)]
		[ProducesResponseType(404)]
		public async Task<ActionResult<GetByIdBookModel>> GetById([FromRoute] Guid id)
		{
			var book = await _mediator.Send(new GetBookByIdQuery(id));

			return book is null ? NotFound() : Ok(_mapper.Map<GetByIdBookModel>(book));
		}

		/// <summary>
		/// Creates a new book
		/// </summary>
		/// <remarks>
		/// Sample request:
		///
		///     POST /api/Book
		///     {
		///			"title": "White Fang",
		///			"genre": "Adventure",
		///			"price": 50,
		///			"authorId": "31ab0d00-55f7-447a-967e-1d009db93b02"
		///		}
		/// </remarks>
		/// <param name="bookDto">The book to create</param>
		/// <returns>The newly created book</returns>
		/// <response code="201">Returns the newly created book</response>
		/// <response code="400">If the input model is invalid</response>
		[HttpPost]
		[ProducesResponseType(typeof(CreateBookModel), 201)]
		[ProducesResponseType(400)]
		public async Task<ActionResult<CreateBookModel>> CreateAsync([FromBody] CreateBookCommand bookDto)
		{
			if (!ModelState.IsValid)
				return BadRequest(ModelState);


			var createdBook = await _mediator.Send(new CreateBookCommand(bookDto.Book));

			return createdBook is null
				? BadRequest("Author was not found.")
				: StatusCode(201, _mapper.Map<CreateBookModel>(createdBook));
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
		[ProducesResponseType(typeof(UpdateBookModel), 200)]
		[ProducesResponseType(404)]
		public async Task<ActionResult<UpdateBookModel>> UpdateAsync([FromRoute] Guid id, [FromBody] UpdateBookCommand bookDto)
		{
			var updatedBook = await _mediator.Send(new UpdateBookCommand(id, bookDto.Book));

			return updatedBook is null ? BadRequest($"Author or book were not found.") : Ok(_mapper.Map<UpdateBookModel>(updatedBook));
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
		public async Task<ActionResult> Delete([FromRoute] Guid id)
		{

			var bookToDelete = await _mediator.Send(new DeleteBookCommand(id));

			return !bookToDelete ? NotFound() : NoContent();
		}
	}
}
