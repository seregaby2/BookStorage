using BookStorage.Models;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography.X509Certificates;
using static System.Reflection.Metadata.BlobBuilder;
using System.Xml.Linq;
using BookStorage.DTO;

namespace BookStorage.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class AuthorController : ControllerBase
    {
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
        [ProducesResponseType(typeof(IEnumerable<AuthorDto>), 200)]
        public ActionResult<IEnumerable<AuthorDto>> GetAll()
        {
            var authorsDto = _authors.Select(a => new AuthorDto
            {
                Id = a.Id,
                FirstName = a.FirstName,
                LastName = a.LastName,
                BirthDate = a.BirthDate
            });
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
        [ProducesResponseType(typeof(AuthorDto), 200)]
        [ProducesResponseType(404)]
        public ActionResult<AuthorDto> GetById([FromRoute] Guid id)
        {
            var author = _authors.FirstOrDefault(a => a.Id == id);
            if (author == null)
            {
                return NotFound();
            }
            var authorDto = new AuthorDto
            {
                Id = author.Id,
                FirstName = author.FirstName,
                LastName = author.LastName,
                BirthDate = author.BirthDate
            };

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
        [ProducesResponseType(typeof(CreateAuthorDto), 201)]
        [ProducesResponseType(400)]
        public ActionResult<CreateAuthorDto> Create([FromBody] CreateAuthorDto authorDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var author = new Author
            {
                Id = Guid.NewGuid(),
                FirstName = authorDto.FirstName,
                LastName = authorDto.LastName,
                BirthDate = authorDto.BirthDate,
            };

            _authors.Add(author);
            return StatusCode(201, author);
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
        [ProducesResponseType(typeof(UpdateAuthorDto), 200)]
        [ProducesResponseType(404)]
        public ActionResult<UpdateAuthorDto> Update([FromRoute] Guid id, [FromBody] UpdateAuthorDto authorDto)
        {
            var existingAuthor = _authors.FirstOrDefault(a => a.Id == id);
            if (existingAuthor == null)
            {
                return NotFound();
            }
            existingAuthor.BirthDate = authorDto.BirthDate;
            existingAuthor.FirstName = authorDto.FirstName;
            existingAuthor.LastName = authorDto.LastName;
            return Ok(existingAuthor);
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
