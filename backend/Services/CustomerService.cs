using backend.MODEL;
using backend.Repositories;
using ModelEntity.MODEL;
using System.Collections.Generic;
using System.Threading.Tasks;
using Serilog;

namespace backend.Services
{
    public class CustomerServices : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerServices(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<IEnumerable<Customer>> GetAllCustomersAsync()
        {
            return await _customerRepository.GetAllCustomersAsync();
        }

            public async Task<IEnumerable<City>> GetAllCitiesAsync()
            {
                return await _customerRepository.GetAllCitiesAsync();
            }

        public async Task<Customer> GetCustomerByIdAsync(int id)
        {
            return await _customerRepository.GetCustomerByIdAsync(id);
        }

        public async Task AddCustomerAsync(Customer customer)
        {
            // ✅ INFO log → goes to DB
            Log.Information(
                "Customer save started. Name: {Name}, Email: {Gmail}",
                customer.Name,
                customer.Gmail
            );

            await _customerRepository.AddCustomerAsync(customer);

            // ✅ INFO log → goes to DB
            Log.Information(
                "Customer saved successfully. CustomerId: {CustomerId}",
                customer.Id
            );
        }
        public async Task UpdateCustomerAsync(Customer updatedCustomer)
        {
            await _customerRepository.UpdateAsync(updatedCustomer);
        }

        public async Task AddContactAsync(Contact Contact)
        {
            // You can add business logic/validation here before adding
            await _customerRepository.AddContactAsync(Contact);
        }
    }
}
