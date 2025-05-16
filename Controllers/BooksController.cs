/*using BookStorage.MockData.MockBook;
using BookStorage.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookStorage.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IMockBookData _bookData;

        public BooksController(IMockBookData bookData)
        {
            _bookData = bookData;
        }

        [HttpGet]
        public Task<ActionResult<IEnumerable<Book>>> GetAllBook()
        {
            return Ok(_bookData.GetAll());
        }
    }
}
*/