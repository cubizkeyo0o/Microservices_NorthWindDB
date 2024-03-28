namespace Customer.Domain.Repositories
{
    using Customer.Domain.Entities;
    public interface ICustomerRepository
    {
        Task<List<CustomerModel>> GetCustomers();
        Task<CustomerModel> GetCustomerById(string id);
        Task<string> GetEmailCustomer(string id);

    }
}
