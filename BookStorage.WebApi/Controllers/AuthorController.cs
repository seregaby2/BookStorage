using AutoMapper;
using BookStorage.Application.Interfaces.Services;
using BookStorage.Domain.Models;
using BookStorage.WebApi.DTOs.Author;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookStorage.WebApi.Controllers
{
	[ApiVersion("1.0")]
	[ApiController]
	[Route("api/v{version:apiVersion}/[controller]")]
	public class AuthorController : ControllerBase
	{
		private readonly IAuthorService _authorService;
		private readonly IMapper _mapper;

		public AuthorController(IAuthorService authorService, IMapper mapper)
		{
			_authorService = authorService;
			_mapper = mapper;
		}

		/// <summary>
		/// Retrieves all authors available in the system
		/// </summary>
		/// <returns>A list of authors</returns>
		/// <response code="200">Returns the list of authors.</response>
		[HttpGet]
		[ProducesResponseType(typeof(IEnumerable<AuthorViewDto>), 200)]
		public async Task<ActionResult<IEnumerable<AuthorViewDto>>> GetAll()
		{
			var authors = await _authorService.GetAll();

			var authorsDto = _mapper.Map<List<AuthorViewDto>>(authors);

			return Ok(authorsDto);
		}

		/// <summary>
		/// Retrieves a specific author by their unique identifier
		/// </summary>
		/// <param name="id">The unique identifier of the author</param>
		/// <returns>The author with the specified ID</returns>
		/// <response code="200">Returns the author</response>
		/// <response code="404">If the author is not found</response>
		[HttpGet("{id}")]
		[ProducesResponseType(typeof(AuthorViewDto), 200)]
		[ProducesResponseType(404)]
		public async Task<ActionResult<AuthorViewDto>> GetById([FromRoute] Guid id)
		{
			var author = await _authorService.GetById(id);
			if (author == null)
				return NotFound();

			var authorDto = _mapper.Map<AuthorViewDto>(author);

			return Ok(authorDto);
		}

		/// <summary>
		/// Creates a new author
		/// </summary>
		/// <remarks>
		/// Sample request:
		///
		///     POST /api/Author
		///     {
		///         "firstName": "Leo",
		///         "lastName": "Tolstoy",
		///         "birthDate": "2025-05-29"
		///     }
		/// </remarks>
		/// <param name="authorDto">The author to create</param>
		/// <returns>The newly created author</returns>
		/// <response code="201">Returns the newly created author</response>
		/// <response code="400">If the input model is invalid</response>
		[Authorize(Roles = "Admin")]
		[HttpPost("admin")]
		[ProducesResponseType(typeof(AuthorViewDto), 201)]
		[ProducesResponseType(400)]
		public async Task<ActionResult<AuthorViewDto>> Create([FromBody] CreateAuthorDto authorDto)
		{
			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			var author = _mapper.Map<Author>(authorDto);

			var createdAuthor = await _authorService.Create(author);
			if (createdAuthor == null)
				return BadRequest("Author with the same name already exists.");

			var createdDto = _mapper.Map<AuthorViewDto>(createdAuthor);

			return StatusCode(201, createdDto);
		}

		/// <summary>
		/// Updates an existing author by their unique identifier
		/// </summary>
		/// <param name="id">The unique identifier of the author to update</param>
		/// <param name="authorDto">The updated author data</param>
		/// <returns>The updated author</returns>
		/// <response code="200">Returns the updated author</response>
		/// <response code="404">If the author with the specified ID is not found</response>
		[HttpPut("{id}")]
		[ProducesResponseType(typeof(AuthorViewDto), 200)]
		[ProducesResponseType(404)]
		public async Task<ActionResult<AuthorViewDto>> Update([FromRoute] Guid id, [FromBody] UpdateAuthorDto authorDto)
		{
			var authorToUpdate = _mapper.Map<Author>(authorDto);

			var updatedAuthor = await _authorService.Update(id, authorToUpdate);
			if (updatedAuthor == null)
				return NotFound();

			var authorViewDto = _mapper.Map<AuthorViewDto>(updatedAuthor);

			return Ok(authorViewDto);
		}

		/// <summary>
		/// Deletes an author by their unique identifier
		/// </summary>
		/// <param name="id">The unique identifier of the author to delete</param>
		/// <response code="204">Author was successfully deleted</response>
		/// <response code="404">Author with the specified ID was not found</response>
		[HttpDelete("{id}")]
		[ProducesResponseType(204)]
		[ProducesResponseType(404)]
		public async Task<ActionResult> Delete([FromRoute] Guid id)
		{
			var isAuthorAndBooksDelete = await _authorService.Delete(id);
			if (!isAuthorAndBooksDelete)
				return NotFound();

			return NoContent();
		}
	}
}
