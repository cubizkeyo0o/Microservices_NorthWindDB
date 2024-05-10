using WebClient.Models;

namespace WebClient.ApiServices
{
    public interface ICustomerService
    {
        Task<CustomerResponse> GetAllCustomer();
        Task<CustomerResponse> GetCustomerById(string customerId);
    }
}
