namespace Customer.Domain.Repositories
{
    using Customer.Domain.Entities;
    public interface ICustomerRepository
    {
        Task<List<CustomerModel>> GetAllCustomers();
        Task<CustomerModel> GetCustomerById(string id);
        Task<string> GetEmailCustomer(string id);

    }
}
