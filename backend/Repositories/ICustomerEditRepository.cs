using backend.MODEL;
using ModelEntity.MODEL;
using System.Collections.Generic;

namespace backend.Repositories
{
    public interface ICustomerEditRepository
    {
        IEnumerable<CustomerEdit> GetAll();
        CustomerEdit GetById(int id);
        void Add(CustomerEdit customer);
    }
}
