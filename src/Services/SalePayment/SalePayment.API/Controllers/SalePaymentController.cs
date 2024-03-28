using MediatR;
using Microsoft.AspNetCore.Mvc;
using SalePayment.Application.Queries;
using SalePayment.Domain.Entities;
using System.Text.Json;
using Newtonsoft.Json;
using System.Net;
using SalePayment.Application.Response;
using AutoMapper;
using EventBus.Messages.Commands;
using System.Collections.ObjectModel;
using SalePayment.Application.Producer.RabbitMQ;


namespace SalePayment.API.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    //[ServiceFilter(typeof(AutomatedRequestMiddleware))]
    public class SalePaymentController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly IMessageProducer _messageProducer;
        private readonly ILogger<SalePaymentController> _logger;

        public SalePaymentController(ILogger<SalePaymentController> logger,IMediator mediator, IMapper mapper,IMessageProducer messageProducer )
        {
            _logger = logger;
            _mediator = mediator;
            _mapper = mapper;
            _messageProducer = messageProducer;
        }

        //setup _option để convert List<T> thành json dễ đọc hơn
        private readonly JsonSerializerOptions _options = new()
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = true,
        };

        [HttpGet]
        [Route("getproductsbyorderid/{id}")]
        public async Task<ActionResult<Order>> GetProductsbyOrderId(int id)
        {
            var query = new GetInvoicesQuery(id);
            var order = await _mediator.Send(query);
            return Ok(order.Orders.OrderDetails);
        }

        [HttpGet]
        [Route("GetOrderById/{id}")]
        public async Task<ActionResult<Order>> GetOrderbyId(int id)
        {
            var query = new GetOrderByIdQuery(id);
            var order = await _mediator.Send(query);
            return Ok(order);
        }

        [HttpGet]
        [Route("GetOrderDetailByOrderId/{id}")]
        public async Task<ActionResult<Order>> GetOrderDetailByOrderId(int orderId)
        {
            var query = new GetOrderDetailByOrderIdQuery(orderId);
            var order = await _mediator.Send(query);
            return Ok(order);
        }

        [HttpGet]
        [Route("GetInvoice/{id}")]
        public async Task<ActionResult<InvoiceResponse>> GetInvoice(int id)
        {
            var query = new GetInvoicesQuery(id);
            var order =await _mediator.Send(query);

            return Ok(order);
        }
        [HttpGet]
        [Route("checkout/{orderId}")]
        public async Task<ActionResult> CheckOut(int orderId)
        {
            //lấy invoice lên và show ra cho custumer thấy
            var query = new GetInvoicesQuery(orderId);
            var Invoice = await _mediator.Send(query);


            //Kh thanh toán hoàn tất, thì sẽ đẩy invoice sang queue để gửi hóa đơn đến mail của customer
            //2 cách map:
            //cách dùng automapper
            var eventMesg = _mapper.Map<InvoiceCheckoutCommand>(Invoice);
            //cách thủ công 
            //var eventMesg2 = new InvoiceCheckoutCommand()
            //{
            //    Orders = _mapper.Map<OrderCmd>(Invoice.Orders),
            //    Details = _mapper.Map<List<OrderDetailCmd>>(Invoice.Details),
            //    ProductInfos = _mapper.Map<List<ProductInfoCmd>>(Invoice.ProductInfos),
            //};
            
            //var Producer = new RabbitMQProducer();
            _messageProducer.SendMessage(eventMesg);
            Console.WriteLine("[x]sent a eventmesg");
            return Ok(eventMesg.Orders);
        }

        //[HttpGet]
        //[Route("sentMessage")]
        //public ActionResult<string> SentMessage()
        //{
        //    var testMesg = new TestMessage() { Message = "hello consumer" };
        //    _messageProducer.SendMessage(testMesg);
        //    return "đã gửi message";
        //}
        
    }
}
