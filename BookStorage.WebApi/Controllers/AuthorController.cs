using AutoMapper;
using BookStorage.Application.Commands.Author.Create;
using BookStorage.Application.Commands.Author.Delete;
using BookStorage.Application.Commands.Author.Update;
using BookStorage.Application.Queries.Author.GetAll;
using BookStorage.Application.Queries.Author.GetById;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookStorage.WebApi.Controllers
{
	[ApiVersion("1.0")]
	[ApiController]
	[Route("api/v{version:apiVersion}/[controller]")]
	public class AuthorController : ControllerBase
	{
		private readonly IMapper _mapper;
		private readonly IMediator _mediator;

		public AuthorController(IMapper mapper, IMediator mediator)
		{
			_mapper = mapper;
			_mediator = mediator;
		}

		/// <summary>
		/// Retrieves all authors available in the system
		/// </summary>
		/// <returns>A list of authors</returns>
		/// <response code="200">Returns the list of authors.</response>
		[HttpGet]
		[ProducesResponseType(typeof(IEnumerable<GetAllAuthorModel>), 200)]
		public async Task<ActionResult<IEnumerable<GetAllAuthorModel>>> GetAll()
		{
			var authors = await _mediator.Send(new GetAllAuthorQuery());

			var authorsDto = _mapper.Map<List<GetAllAuthorModel>>(authors);

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
		[ProducesResponseType(typeof(GetByIdAuthorModel), 200)]
		[ProducesResponseType(404)]
		public async Task<ActionResult<GetByIdAuthorModel>> GetById([FromRoute] Guid id)
		{
			var author = await _mediator.Send(new GetAuthorByIdQuery(id));

			return author is null ? NotFound() : Ok(_mapper.Map<GetByIdAuthorModel>(author));
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
		[HttpPost]
		[ProducesResponseType(typeof(CreateAuthorModel), 201)]
		[ProducesResponseType(400)]
		public async Task<ActionResult<CreateAuthorModel>> Create([FromBody] CreateAuthorCommand authorDto)
		{
			if (!ModelState.IsValid)
				return BadRequest(ModelState);

			var createdAuthor = await _mediator.Send(new CreateAuthorCommand(authorDto.Author));

			return createdAuthor is null
				? BadRequest("Author with the same name already exists.")
				: StatusCode(201, _mapper.Map<CreateAuthorModel>(createdAuthor));
		}

		/// <summary>
		/// Updates an existing author by their unique identifier
		/// </summary>
		/// <param name="id">The unique identifier of the author to update</param>
		/// <param name="authorDto">The updated author data</param>
		/// <returns>The updated author</returns>
		/// <response code="200">Returns the updated author</response>
		/// <response code="404">If the author with the specified ID is not found</response>
		[Authorize(Roles = "Admin")]
		[HttpPut("{id}")]
		[ProducesResponseType(typeof(UpdateAuthorModel), 200)]
		[ProducesResponseType(404)]
		public async Task<ActionResult<UpdateAuthorModel>> Update([FromRoute] Guid id, [FromBody] UpdateAuthorCommand authorDto)
		{
			var updatedAuthor = await _mediator.Send(new UpdateAuthorCommand(id, authorDto.Author));

			return updatedAuthor is null ? NotFound($"Author was not found.") : Ok(_mapper.Map<UpdateAuthorModel>(updatedAuthor));
		}

		/// <summary>
		/// Deletes an author by their unique identifier
		/// </summary>
		/// <param name="id">The unique identifier of the author to delete</param>
		/// <response code="204">Author was successfully deleted</response>
		/// <response code="404">Author with the specified ID was not found</response>
		[Authorize(Roles = "Admin")]
		[HttpDelete("{id}")]
		[ProducesResponseType(204)]
		[ProducesResponseType(404)]
		public async Task<ActionResult> Delete([FromRoute] Guid id)
		{
			var authorToDelete = await _mediator.Send(new DeleteAuthorCommand(id));

			return !authorToDelete ? NotFound() : NoContent();
		}
	}
}
