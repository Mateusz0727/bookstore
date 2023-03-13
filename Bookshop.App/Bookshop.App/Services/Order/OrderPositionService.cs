using AutoMapper;
using Bookshop.App.Models.Order;
using Bookshop.App.Services.Book;
using Bookshop.App.Services.User;
using Bookshop.Data.Extensions;
using Bookshop.Data.Model;

namespace Bookshop.App.Services.Order
{
    public class OrderPositionService : BaseService
    {
        protected BookService _bookService;
        public OrderPositionService(IMapper mapper, BaseContext context,BookService bookService) : base(mapper, context)
        {
            _bookService = bookService;
        }
        public OrderFormModel AddPosition(OrderFormModel order)
        {
            
            foreach (var posiotion in order.Positions)
            {
                var p = Mapper.Map<Data.Model.OrderPosition>(posiotion);
                var o = Mapper.Map<Data.Model.Order>(order);
                p.Order = o;
                Data.Model.Book book = _bookService.GetAll().FirstOrDefault(p => p.Id == posiotion.Book_Id);
                if (book == null)
                {
                    throw new Exception("nie znaleziono produktu");
                }
                p.PublicId = Guid.NewGuid().ToString();
              
              
            }
         
            return order;
        }
    }
}
