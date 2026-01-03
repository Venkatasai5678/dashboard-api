using backend.Data;
using backend.MODEL;
using ModelEntity.MODEL;
using System.Collections.Generic;
using System.Linq;

namespace backend.Repositories
{
    public class CustomerEditRepository : ICustomerEditRepository
    {
        private readonly AppDbContext _context;

        public CustomerEditRepository(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<CustomerEdit> GetAll()
        {
            return _context.CustomerEdits.ToList();
        }

        public CustomerEdit GetById(int id)
        {
            return _context.CustomerEdits.Find(id);
        }

        public void Add(CustomerEdit customer)
        {
            _context.CustomerEdits.Add(customer);
            _context.SaveChanges();
        }
    }
}
