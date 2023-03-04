using AutoMapper;
using Bookshop.App.Models.Order;
using Bookshop.Context;
using Bookshop.Data.Extensions;
using Bookshop.Data.Model;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using static System.Reflection.Metadata.BlobBuilder;

namespace Bookshop.App.Services.Order
{
    public class OrderService:BaseService
    {
        public OrderService(BaseContext context, IMapper mapper) : base(mapper, context)
        {

        }
        public Data.Model.Order Create(OrderFormModel order,ClaimsPrincipal user)
        {
          
            var entity = Mapper.Map<Data.Model.Order>(order);
            entity.UserId = user.Id();
            entity.PublicId = Guid.NewGuid().ToString();
            entity.Status = "CREATED";
            Context.Add<Data.Model.Order>(entity);
            Context.SaveChanges();
            return entity;
           
        }

        #region GetAll()
        public List<OrderFormModel> GetAll()
        {
            var orders = Context.Orders.Include(p => p.OrderPositions).ToList();

            return Mapper.Map<List<OrderFormModel>>(orders);
         }
        #endregion
        #region Get()
        public Data.Model.Order Get(long id)
        {
            var order = Context.Orders.Include(p=>p.OrderPositions).Where(x=>x.Id== id).FirstOrDefault();
            return order;
        }
        #endregion
        #region UpdateStaus()
        public void UpdateStatus(long id,string status)
        {
            var order = Get(id);
            order.Status =status;
            Context.Update(order);
            Context.SaveChanges();
        }
        #endregion
    }
}
