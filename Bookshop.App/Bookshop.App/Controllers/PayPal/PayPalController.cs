
using Bookshop.App.Services.Order;
using Bookshop.App.Services.Shipment;
using Bookshop.Configuration.Paypal;
using Bookshop.Data.Model;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using PayPal.Api;
using PayPalCheckoutSdk.Core;
using PayPalCheckoutSdk.Orders;
using PayPalHttp;


namespace Bookshop.App.Controllers.Paypal
{
    [ApiController]
    [Route("[controller]")]
    [EnableCors("_myAllowSpecificOrigins")]
    public class PayPalController : Controller
    {
        private readonly ShipmentService _shipmentService;
        private readonly OrderService _orderService;

        public PayPalController(ShipmentService shipmentService, OrderService orderService)
        {
            _shipmentService = shipmentService;
            _orderService = orderService;
        }

        /*[HttpGet]
        public async Task<ActionResult> CreatePayment()
        {
            var result = await _shipmentService.CreatePayment();
            foreach (var link in result.links)
            {
                if (link.rel.Equals("approval_url"))
                {
                    return Redirect(link.href);
                }
            }
            return NotFound();
        }
        [HttpGet]
        [Route("success")]
        public async Task<ActionResult> ExecutePayment(string paymentId, string token, string PayerID)
        {
            Payment result = await _shipmentService.ExecutePayment(PayerID, paymentId);
            return Ok(result);
        }*/
        #region CreateOrder
        [HttpGet("CreateOrder/{id}")]
        public async Task<ActionResult> CreateOrder(long id)
        {
            try
            {
                var response = await _shipmentService.CreateOrder(id);

                var statusCode = response.StatusCode;

                if (statusCode.ToString().ToLower() == "created")
                {
                    var result = response.Result<PayPalCheckoutSdk.Orders.Order>();
                    foreach (var link in result.Links)
                    {
                        if (link.Rel.Equals("approve"))
                        {
                          
                            return Ok(link.Href);
                        }
                    }
                    return BadRequest();
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        #endregion
        #region CaptureOrder()
        [HttpPost("CaptureOrder")]
        public async Task<ActionResult<Data.Model.Order>> CaptureOrder([FromBody] long orderId)
        {
            try
            {
                var response = await _shipmentService.CaptureOrder(orderId.ToString());
                var statusCode = response.StatusCode;

                if (statusCode.ToString().ToLower() == "created")
                {
                    var result = response.Result<PayPalCheckoutSdk.Orders.Order>();
                    _orderService.UpdateStatus(orderId, result.Status);
                    return Ok(result);
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        #endregion




        [HttpPost("create-payment")]
        public async Task<ActionResult> CreatePayment_()
        {
            var environment = new SandboxEnvironment(PaypalConfiguration.ClientId, PaypalConfiguration.ClientSecret);
            var client = new PayPalHttpClient(environment);
            var order = new OrderRequest()
            {
                CheckoutPaymentIntent = "CAPTURE",
                PurchaseUnits = new List<PurchaseUnitRequest>()
                {
                     new PurchaseUnitRequest()
                        {
                            AmountWithBreakdown = new AmountWithBreakdown()
                            {
                                CurrencyCode = "USD",
                                Value ="120",
                              /*  Breakdown = new AmountBreakdown()
                                {
                                    ItemTotal = new Money()
                                    {
                                        CurrencyCode = "USD",
                                        Value = request.Price.ToString()
                                    }
                                }*/
                            }
                        }
                }
            };

            var response = await client.Execute<OrdersCreateRequest>(new OrdersCreateRequest().RequestBody(order));

            return Ok(response);
        }
    }
}


