using AutoMapper;
using Bookshop.App.Models.Auth;
using Bookshop.App.Models.Book;
using Bookshop.Data.Model;

namespace Bookshop.App.Models
{
    public  class AutoMapperInitializator:Profile
    {
        public AutoMapperInitializator()
        {
            BookModels();
            UserModels();
        }
        protected void BookModels()
        {
            CreateMap<Data.Model.Book,BookFormModel>().ReverseMap();
                
        }
        protected void UserModels()
        {
            CreateMap<Data.Model.User, RegisterFormModel>().ReverseMap();
        }
    }
}
