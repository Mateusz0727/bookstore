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
        private readonly OrderPositionService _orderPositionService;

        public OrderService(BaseContext context, IMapper mapper,OrderPositionService orderPositionService) : base(mapper, context)
        {
            _orderPositionService = orderPositionService;
        }
        public OrderFormModel Create(OrderFormModel order,ClaimsPrincipal user)
        {
          
            var entity = Mapper.Map<Data.Model.Order>(order);
            entity.UserId = user.Id();
            entity.PublicId = Guid.NewGuid().ToString();
            entity.Status = "CREATED";
            foreach (var position in entity.OrderPositions)
            {
                position.PublicId = Guid.NewGuid().ToString();
            }
            Context.Add<Data.Model.Order>(entity);
            Context.SaveChanges();
            _orderPositionService.AddPosition(order);
            order = Mapper.Map<OrderFormModel>(order);
            return order;
           
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
        #region GetByPayPalId
        public Data.Model.Order GetByPayPalId(string id)
        {
            var order = Context.Orders.Include(p => p.OrderPositions).Where(x => x.PayPalId == id).FirstOrDefault();
            return order;
        }
        #endregion
        #region UpdateStaus()
        public void UpdateStatus(string id,string status)
        {
            var order = GetByPayPalId(id);
            order.Status =status;
            Context.Update(order);
            Context.SaveChanges();
        }
        #endregion
        public void UpdatePayPalId(long id,string payPalId)
        {
            var order = Get(id);
            order.PayPalId = payPalId;
            Context.Update(order);
            Context.SaveChanges();
        }
    }
}
