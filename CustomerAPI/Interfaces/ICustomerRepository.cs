using CustomerAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerAPI.Interfaces
{
    public interface ICustomerRepository
    {
        ICollection<Customer> GetAllCustomers();
        Customer GetCustomerById(int id);
        bool CustomerExists(int id);
        bool CreateCustomer(Customer customer);
        bool UpdateCustomer(Customer customer);
        bool DeleteCustomer(Customer customer);
        bool Save();


    }
}
