using AutoMapper;
using Bookshop.App.Models.Auth;
using Bookshop.App.Models.Book;
using Bookshop.App.Models.Order;
using Bookshop.Data.Model;

namespace Bookshop.App.Models
{
    public  class AutoMapperInitializator:Profile
    {
        public AutoMapperInitializator()
        {
            BookModels();
            UserModels();
            OrderModels();
        }
        protected void BookModels()
        {
            CreateMap<Data.Model.Book,BookFormModel>().ReverseMap();
                
        }
        protected void UserModels()
        {
            CreateMap<Data.Model.User, RegisterFormModel>().ReverseMap();
        }
        protected void OrderModels()
        {
            CreateMap<Data.Model.Order, OrderFormModel>().ForMember(x=>x.Positions,m=>m.MapFrom(s=>s.OrderPositions))
              
                .ReverseMap();
            CreateMap<Data.Model.OrderPosition, OrderPositionFormModel>()
                .ForMember(x=>x.Book_Id,m=>m.MapFrom(s=>s.BookId))
                .ReverseMap();
        }
    }
}
