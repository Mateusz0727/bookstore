using AutoMapper;
using Bookshop.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookshop.Data.Extensions
{
    public abstract class BaseService 
    {
        protected BaseContext Context { get;  }
        protected IMapper Mapper { get; }

        public BaseService(IMapper mapper,BaseContext context)
        {
            Mapper = mapper;
            Context = context;
            
        }
      
    }
}
