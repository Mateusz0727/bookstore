using AutoMapper;
using Bookshop.App.Models.Book;
using Bookshop.App.Services.Book;
using Bookshop.Data.Model;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using static System.Reflection.Metadata.BlobBuilder;

namespace Bookshop.App.Controllers.Book
{

    [ApiController]
    [Route("[controller]")]
    [EnableCors("_myAllowSpecificOrigins")]
    public class BookController : Controller
    {


        private readonly BookService _bookService;

        public IMapper Mapper { get; }

        public BookController(BookService bookService, IMapper Mapper)
        {
            _bookService = bookService;
            this.Mapper = Mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<BookFormModel>>> GetAll()
        {
            var books = _bookService.GetAll();

            var result = Mapper.Map<List<BookFormModel>>(books);
            return result;
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<BookFormModel>> Get(long id)
        {
            var book = _bookService.Get(id);
            if (book != null)
                return GetResource(book);
            else return BadRequest();
        }

        private BookFormModel GetResource(Data.Model.Book entity)
        {
            return Mapper.Map<BookFormModel>(entity);
        }
    }
}