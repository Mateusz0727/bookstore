using AutoMapper;
using Bookshop.App.Services.Order;
using Bookshop.Configuration;
using Bookshop.Configuration.Paypal;
using Bookshop.Data.Extensions;
using Bookshop.Data.Model;
using MailKit.Search;
using Microsoft.AspNetCore.Http;
using PayPal.Api;
using PayPalCheckoutSdk.Core;
using PayPalCheckoutSdk.Orders;
using PayPalCheckoutSdk.Payments;
using System.Globalization;
using  _paypalhhtp=PayPalHttp;

namespace Bookshop.App.Services.Shipment
{

    public class ShipmentService : BaseService
    {
        private readonly PaypalConfig _config;
        private OrderService _orderService;

        public ShipmentService(BaseContext context, IMapper mapper,PaypalConfig config,OrderService orderService) : base(mapper, context)
        {
            _config = config;
            _orderService = orderService;
        }

      /*  public async Task<Payment> CreatePayment()
        {
            var apiContext = PaypalConfiguration.GetAPIContext();
           
            var createdPayment = new Payment();
            try
            {
                Payment payment = new Payment()
                {
                    intent = "sale",
                    payer = new PayPal.Api.Payer { payment_method = "paypal" },
                    transactions = new List<Transaction>()
                    {
                        new Transaction()
                        {
                            amount = new Amount
                            {
                                currency = "PLN",
                                total = "100"
                            },
                            description="Test product"
                        }
                    },
                    redirect_urls = new RedirectUrls
                    {
                        cancel_url = "https://localhost:4000/cancel",
                        return_url = "https://localhost:4000/PayPal/success"
                    }
                };
                createdPayment = payment.Create(apiContext);
                
            }
            catch (Exception ex)
            {

            }
            return createdPayment;
        }

        public async Task<Payment> ExecutePayment(string payerID, string paymentID)
        {
            var apiContext = PaypalConfiguration.GetAPIContext();
            PaymentExecution paymentExecution = new PaymentExecution() { payer_id = payerID };
            Payment payment = new Payment() { id = paymentID };
            Payment executePayment = payment.Execute(apiContext, paymentExecution);
            return executePayment;
        }*/

        public async Task<_paypalhhtp.HttpResponse> CreateOrder(long id)
        {
            var request = _orderService.Get(id);
            var environment = new SandboxEnvironment(PaypalConfiguration.ClientId, PaypalConfiguration.ClientSecret);
            var client = new PayPalHttpClient(environment);
            
            var order = new OrderRequest()
            {
                CheckoutPaymentIntent = "CAPTURE",
                ApplicationContext = new ApplicationContext()
                {                   
                    UserAction = "PAY_NOW",
                    ReturnUrl = "https://localhost:4000/PayPal/CaptureOrder",
                    CancelUrl = "https://localhost/cancel"
                },
                PurchaseUnits = new List<PurchaseUnitRequest>()
                    {
                        new PurchaseUnitRequest()
                        {
                            AmountWithBreakdown = new AmountWithBreakdown()
                            {
                                CurrencyCode = "PLN",
                                Value = request.Amount.ToString(new CultureInfo("en-US")),
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
            
            var createOrderRequest = new OrdersCreateRequest();
            createOrderRequest.Prefer("return=representation");
            createOrderRequest.RequestBody(order);
            var response = await client.Execute(createOrderRequest);
            return response;
        }
        public async Task<_paypalhhtp.HttpResponse> CaptureOrder(string orderId)
        {
            var environment = new SandboxEnvironment(PaypalConfiguration.ClientId, PaypalConfiguration.ClientSecret);
            var client = new PayPalHttpClient(environment);

            var request = new OrdersCaptureRequest(orderId);
            request.RequestBody(new OrderActionRequest());

            return await client.Execute(request);
        }
    }
}

