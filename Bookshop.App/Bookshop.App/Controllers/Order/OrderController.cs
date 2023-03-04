using AutoMapper;
using Bookshop.App.Models.Book;
using Bookshop.App.Models.Order;
using Bookshop.App.Services.Book;
using Bookshop.App.Services.Order;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace Bookshop.App.Controllers.Order
{
    [Authorize(Policy = "JwtPolicy")]
    [ApiController]
    [Route("[controller]")]
    [EnableCors("_myAllowSpecificOrigins")]
   
    public class OrderController : Controller
    {
        private OrderService _orderService;

        public OrderController(OrderService orderService)
        {
            _orderService = orderService;
        }
        [HttpPost]
        public async Task< IActionResult >Create([FromBody] OrderFormModel order)
        {

            var newPublicId = Guid.NewGuid();
            var entity = _orderService.Create(order, User);
            return Created("~/PayPal/CreateOrder", entity);
        }
        [HttpGet]
        public async Task<ActionResult<List<OrderFormModel>>> GetAll()
        {
            var result =_orderService.GetAll();

           
            return result;
        }
        

    }
}
