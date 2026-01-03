using backend.MODEL;
using backend.MODEL;
using backend.Repositories;
using ModelEntity.MODEL;
using System.Collections.Generic;

namespace backend.Services
{
    public class CustomerEditService : ICustomerEditService
    {
        private readonly ICustomerEditRepository _repo;


        public CustomerEditService(ICustomerEditRepository repo)
        {
            _repo = repo;
        }

        public IEnumerable<CustomerEdit> GetAllCustomers() => _repo.GetAll();

        public CustomerEdit GetCustomerById(int id) => _repo.GetById(id);
    }
}
