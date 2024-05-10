using WebClient.Models;

namespace WebClient.ApiServices
{
    public class CustomerService : ICustomerService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public CustomerService(IHttpClientFactory httpClientFactory, IHttpContextAccessor httpContextAccessor)
        {
            _httpClientFactory = httpClientFactory;
            _httpContextAccessor = httpContextAccessor;
        }

        public Task<CustomerResponse> GetAllCustomer()
        {

            throw new NotImplementedException();
        }

        public Task<CustomerResponse> GetCustomerById(string customerId)
        {
            throw new NotImplementedException();
        }
    }
}
