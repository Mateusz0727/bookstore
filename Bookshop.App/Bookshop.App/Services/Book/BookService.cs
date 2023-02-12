using Bookshop.Data.Extensions;
using Bookshop.Data;
using Bookshop.Data.Model;
using Microsoft.AspNetCore.Identity;
using Bookshop.Mailing;
using AutoMapper;
using Bookshop.App.Models.Book;

namespace Bookshop.App.Services.Book
{
    public class BookService : BaseService
    {
        public BookService(BaseContext context,IMapper mapper) : base(mapper,context)
        {
        }
        #region GetAll()
        public List<Data.Model.Book> GetAll()
        {
            return Context.Books.OrderBy(x => x.Title).ToList();
        }
        #endregion
        #region GetBook()
        public Data.Model.Book Get(long id) 
        {
            var book = Context.Books.FirstOrDefault(x=>x.Id == id);

            return book == null ? null : book;
        }
        #endregion
        #region GetPopular()
        public Data.Model.Book GetPopular(long id)
        {
            var book = Context.Books.FirstOrDefault(x => x.Id == id);

            return book == null ? null : book;
        }
        #endregion
        /* #region Create()
         public Data.Model.Book Create(BookFormModel formModel)
         {
             var entity = Mapper.Map<Data.Model.Book>(formModel);
             return entity;
         }
         #endregion*/
    }
}
