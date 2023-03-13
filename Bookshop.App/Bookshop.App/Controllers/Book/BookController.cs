using AutoMapper;
using Bookshop.App.Models.Book;
using Bookshop.App.Services.Book;
using Bookshop.App.Services.Resource;
using Bookshop.Context;
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
        private readonly ResourceService _resourceService;

        public IMapper Mapper { get; }

        public BookController(BookService bookService, IMapper Mapper, ResourceService resourceService)
        {
            _bookService = bookService;
            this.Mapper = Mapper;
            _resourceService = resourceService;
        }

        [HttpGet]
        public async Task<ActionResult<List<BookFormModel>>> GetAll()
        {
            var books = _bookService.GetAll();

            var result = Mapper.Map<List<BookFormModel>>(books);
            foreach(var book in result)
            {
                book.ImageUrl = _resourceService.Get(book.Id);
            }
            return result;
        }
        [HttpGet("popular")]
        public async Task<ActionResult<List<BookFormModel>>> GetPopular()
        {
            var books = _bookService.GetPopular();

            var result = Mapper.Map<List<BookFormModel>>(books);
            foreach (var book in result)
            {
                book.ImageUrl = _resourceService.Get(book.Id);
            }
            return result;
        }
        [HttpGet("new")]
        public async Task<ActionResult<List<BookFormModel>>> GetNew()
        {
            var books = _bookService.GetNew();

            var result = Mapper.Map<List<BookFormModel>>(books);
            foreach (var book in result)
            {
                book.ImageUrl = _resourceService.Get(book.Id);
            }
            return result;
        }
        [HttpGet("language")]
        public async Task<ActionResult<List<BookFormModel>>> GetBookByLanguage()
        {
            var books = _bookService.GetBookByLanguage("pl");

            var result = Mapper.Map<List<BookFormModel>>(books);
            foreach (var book in result)
            {
                book.ImageUrl = _resourceService.Get(book.Id);
            }
            return result;
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<BookFormModel>> Get(long id)
        {
            var book = _bookService.Get(id);
            if (book != null)
            {
               var entity = GetResource(book);
                entity.ImageUrl = _resourceService.Get(book.Id);
                return entity;
            }

            else return BadRequest();
        }

        private BookFormModel GetResource(Data.Model.Book entity)
        {
            return Mapper.Map<BookFormModel>(entity);
        }
    }
}