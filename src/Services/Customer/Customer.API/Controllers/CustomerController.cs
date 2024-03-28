using Customer.Application.Consumer.RabbitMQ;
using Customer.Application.Queries;
using Customer.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Customer.API.Controllers
{
    [ApiController]
    //Cẩn thận route nhầm vào Microsoft.AspNetCore.Component
    [Route("api/[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly IMessageConsumer _messageConsumer;
        private readonly IMediator _mediator;
        public CustomerController(IMessageConsumer messageConsumer, IMediator mediator)
        {
            _messageConsumer = messageConsumer;
            _mediator = mediator;
        }


        [HttpGet]
        [Route("GetCustomerById/{id}")]
        public async Task<ActionResult<CustomerModel>> GetCustomerById(string id)
        {
            var query = new GetCustomerByIdQuery(id);
            return await _mediator.Send(query);
        }

        [HttpGet]
        [Route("sendmail")]
        public async Task<ActionResult<string>> SendMail()
        {
            var checkSendMail = await _messageConsumer.Test();
            if(checkSendMail != true)
            {
                Console.WriteLine("gửi mail thất bại");
            }
            
            return "Đã send mail";
        }
    }
}
