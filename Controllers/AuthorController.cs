using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using BookStorage.WebApi.DTOs.Author;
using BookStorage.Domain.Models;

namespace BookStorage.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthorController : ControllerBase
    {
        private readonly IMapper _mapper;

        public AuthorController(IMapper mapper)
        {
            _mapper = mapper;
        }

        public static readonly List<Author> _authors = new List<Author>
        {
            new Author 
            {
                Id = Guid.NewGuid(),
                FirstName = "Jack",
                LastName = "London",
                BirthDate = new DateTime(1876, 12, 1),
                Books = new List<Book>()
            },
            new Author
            {
                Id = Guid.NewGuid(),
                FirstName = "Leo",
                LastName = "Tolstoy",
                BirthDate = new DateTime(1910, 11, 20),
                Books = new List<Book>()
            }
        };

        /// <summary>
        /// Retrieves all authors available in the system
        /// </summary>
        /// <returns>A list of authors</returns>
        /// <response code="200">Returns the list of authors.</response>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<AuthorViewDto>), 200)]
        public ActionResult<IEnumerable<AuthorViewDto>> GetAll()
        {
            var authorsDto = _mapper.Map <List<AuthorViewDto>>(_authors);

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
        public ActionResult<AuthorViewDto> GetById([FromRoute] Guid id)
        {
            var author = _authors.FirstOrDefault(a => a.Id == id);
            if (author == null)
            {
                return NotFound();
            }

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
        ///     }
        /// </remarks>
        /// <param name="authorDto">The author to create</param>
        /// <returns>The newly created author</returns>
        /// <response code="201">Returns the newly created author</response>
        /// <response code="400">If the input model is invalid</response>
        [HttpPost]
        [ProducesResponseType(typeof(AuthorViewDto), 201)]
        [ProducesResponseType(400)]
        public ActionResult<AuthorViewDto> Create([FromBody] CreateAuthorDto authorDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var author = _mapper.Map<Author> (authorDto);
            author.Id = Guid.NewGuid();
            author.Books = new List<Book>();
            
            _authors.Add(author);

            var createdDto = _mapper.Map<AuthorViewDto>(author);

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
        public ActionResult<AuthorViewDto> Update([FromRoute] Guid id, [FromBody] UpdateAuthorDto authorDto)
        {
            var existingAuthor = _authors.FirstOrDefault(a => a.Id == id);
            if (existingAuthor == null)
            {
                return NotFound();
            }

            _mapper.Map(authorDto, existingAuthor);

            var updatedDto = _mapper.Map<AuthorViewDto>(existingAuthor);

            return Ok(updatedDto);
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
        public ActionResult Delete([FromRoute] Guid id)
        {
            var authorToDelete = _authors.FirstOrDefault(a => a.Id == id);
            if (authorToDelete == null)
            {
                return NotFound();
            }

            _authors.Remove(authorToDelete);

            return NoContent();
        }
    }
}
