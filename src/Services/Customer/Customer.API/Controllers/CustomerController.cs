using Customer.Application.Consumer.RabbitMQ;
using Customer.Application.Queries;
using Customer.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
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
        [Route("getallcustomers")]
        public async Task<ActionResult<List<CustomerModel>>> GetAllCustomers()
        {
            var query = new GetAllCustomersQuery();
            return await _mediator.Send(query);
            
        }

        [HttpGet]
        [Route("GetCustomerById/{id}")]
        [Authorize(Policy = "Admin")]
        public async Task<ActionResult<CustomerModel>> GetCustomerById(string id)
        {
            var query = new GetCustomerByIdQuery(id);
            return await _mediator.Send(query);
        }

        [HttpGet]
        [Route("sendmail")]
        public async Task<ActionResult<string>> SendMail()
        {
            var checkSendMail = await _messageConsumer.ConsumeMesg();
            if(checkSendMail != true)
            {
                Console.WriteLine("gửi mail thất bại");
            }
            
            return "Đã send mail";
        }
    }
}
