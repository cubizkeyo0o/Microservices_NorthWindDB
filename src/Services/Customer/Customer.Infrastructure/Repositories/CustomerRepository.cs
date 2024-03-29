using Customer.Domain.Entities;
using Customer.Domain.Repositories;
using Customer.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;


namespace Customer.Infrastructure.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly CustomerContext _context;
        public CustomerRepository(CustomerContext context)
        {
            _context = context;
        }
        public async Task<CustomerModel> GetCustomerById(string id)
        {
            var customer = await _context.Customers.Where(c => c.CustomerId == id).FirstAsync();
            if(customer == null)
            {
                throw new NotImplementedException();
            }
            return customer;
        }

        public async Task<List<CustomerModel>> GetAllCustomers()
        {
            var listCustomers =await _context.Customers.ToListAsync();
            if(listCustomers == null)
            {
                throw new NotImplementedException();
            }
            return listCustomers;
        }

        public async Task<string> GetEmailCustomer(string id)
        {
            return await _context.Database.SqlQuery<string>($"SELECT C.Gmail FROM Customers C WHERE C.CustomerID = {id}").FirstAsync();
        }
    }
}
