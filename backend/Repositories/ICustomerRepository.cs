using backend.MODEL;
using ModelEntity.MODEL;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace backend.Repositories
{
    public interface ICustomerRepository
    {
        Task<IEnumerable<Customer>> GetAllCustomersAsync();
        Task<IEnumerable<City>> GetAllCitiesAsync();
        Task<Customer> GetCustomerByIdAsync(int id);
        Task AddCustomerAsync(Customer customer);
        Task AddContactAsync(Contact Contact);

        Task UpdateAsync(Customer updatedCustomer);
        // Other methods like Update, Delete if needed
    }
}
