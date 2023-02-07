using AutoMapper;
using Bookshop.App.Models.Book;
using Bookshop.Data.Model;

namespace Bookshop.App.Models
{
    public  class AutoMapperInitializator:Profile
    {
        public AutoMapperInitializator()
        {
            BookModels();
        }
        protected void BookModels()
        {
            CreateMap<Data.Model.Book,BookFormModel>().ReverseMap();
                
        }
    }
}
