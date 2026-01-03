using backend.Data;
using backend.MODEL;
using Microsoft.EntityFrameworkCore;
using ModelEntity.MODEL;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace backend.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly AppDbContext _context;

        public CustomerRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<City>> GetAllCitiesAsync()
        {
            return await _context.Cities.AsNoTracking().ToListAsync();
        }
        public async Task<IEnumerable<Customer>> GetAllCustomersAsync()
        {
            return await _context.CustomerDetails.ToListAsync();
        }
        public async Task<Customer> GetCustomerByIdAsync(int id)
        {
            return await _context.CustomerDetails.FindAsync(id);
        }
        public async Task AddCustomerAsync(Customer customer)
        {
            _context.CustomerDetails.Add(customer);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateAsync(Customer updatedCustomer)
        {
            var existingCustomer = await _context.CustomerDetails.FindAsync(updatedCustomer.Id);
            if (existingCustomer == null) throw new Exception("Customer not found");

            existingCustomer.Name = updatedCustomer.Name;
            existingCustomer.MobNo = updatedCustomer.MobNo;
            existingCustomer.CityId = updatedCustomer.CityId;
            existingCustomer.Gender = updatedCustomer.Gender;
            existingCustomer.DateOfBirth = updatedCustomer.DateOfBirth;

            await _context.SaveChangesAsync();
        }
        public async Task AddContactAsync(Contact Contact)
        {
            _context.contacts.Add(Contact);
            await _context.SaveChangesAsync();
        }
    }
}