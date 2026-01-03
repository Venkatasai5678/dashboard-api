using backend.MODEL;
using backend.MODEL;
using ModelEntity.MODEL;
using System.Collections.Generic;

namespace backend.Services
{
    public interface ICustomerEditService
    {
        IEnumerable<CustomerEdit> GetAllCustomers();
        CustomerEdit GetCustomerById(int id);
    }
    }

