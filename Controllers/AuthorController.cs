using BookStorage.Models;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography.X509Certificates;
using static System.Reflection.Metadata.BlobBuilder;
using System.Xml.Linq;

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

        [HttpGet]
        public ActionResult<IEnumerable<Author>> GetAll()
        {
            return Ok(_authors);
        }

        [HttpGet("{id}")]
        public ActionResult<Author> GetById([FromRoute] Guid id)
        {
            var author = _authors.FirstOrDefault(a=>a.Id==id);
            if (author == null)
            {
                return NotFound();
            }
            return Ok(author);
        }

        [HttpPost]
        public ActionResult<Author> Create ([FromBody] Author author)
        {
           _authors.Add(author);
            return Ok(author);
        }

        [HttpPut("{id}")]
        public ActionResult<Author> Update([FromRoute] Guid id, [FromBody] Author author)
        {
            var existingAuthor = _authors.FirstOrDefault(a => a.Id == id);
            if (existingAuthor == null) { 
                return NotFound();
            }
            existingAuthor.BirthDate = author.BirthDate;
            existingAuthor.FirstName = author.FirstName;
            existingAuthor.LastName = author.LastName;
            return Ok(author);
        }

        [HttpDelete("{id}")]

        public ActionResult Delete([FromRoute] Guid id) {
            var authorToDelete = _authors.FirstOrDefault(a => a.Id == id);
            if (authorToDelete == null)
            {
                return NotFound();
            }
            _authors.Remove(authorToDelete);
            return Ok();
            
        }

        
    }
}
