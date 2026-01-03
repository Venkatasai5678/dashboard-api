using backend.MODEL;
using ModelEntity.MODEL;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace backend.Services
{
    public interface ICustomerService
    { 
        Task<IEnumerable<Customer>> GetAllCustomersAsync();
        Task<IEnumerable<City>> GetAllCitiesAsync();
        Task<Customer> GetCustomerByIdAsync(int id);
        Task AddCustomerAsync(Customer customer);
        Task AddContactAsync(Contact Contact);
        Task UpdateCustomerAsync(Customer updatedCustomer);
    }
}
