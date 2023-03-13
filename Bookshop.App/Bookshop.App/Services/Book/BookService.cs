using Bookshop.Data.Extensions;
using Bookshop.Data;
using Bookshop.Data.Model;
using Microsoft.AspNetCore.Identity;
using Bookshop.Mailing;
using AutoMapper;
using Bookshop.App.Models.Book;
using Microsoft.EntityFrameworkCore;

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
            return Context.Books.OrderBy(x => x.Id).ToList();
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
        public List<Data.Model.Book> GetPopular()
        {
         var popularIds=   Context.OrderPositions.GroupBy(x => x.BookId).OrderByDescending(q => q.Count()).Take(10).Select(x => x.Key).ToList();
            if(popularIds.Count<10)
            {
                popularIds.AddRange(Context.Books.Select(x=>x.Id).Where(x=>!popularIds.Contains(x)).Take(10-popularIds.Count()).ToList());
            }
           var b= Context.Books.Where(x => popularIds.Contains(x.Id));
            return b.ToList();
          /*  return Context.Books.OrderBy(x => x.Id).ToList();*/
            
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
