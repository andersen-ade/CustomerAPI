using CustomerAPI.Interfaces;
using CustomerAPI.Models;
using CustomerAPI.Models.DTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerAPI.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly CompanyDBContext _context;

        public CustomerRepository(CompanyDBContext context)
        {
            _context = context;
        }
        public bool CreateCustomer(Customer customer)
        {
            _context.Add(customer);
            return Save();
        }

        public bool CustomerExists(int id)
        {
            return _context.Customers.Any(c => c.Id == id);
        }

        public bool DeleteCustomer(Customer customer)
        {
            _context.Remove(customer);
            return Save();
        }

        public ICollection<Customer> GetAllCustomers()
        {
            return _context.Customers.ToList();
        }

        public Customer GetCustomerById(int id)
        {
            return _context.Customers.Where(b => b.Id == id).FirstOrDefault(); 
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateCustomer(Customer customer)
        {
            _context.Update(customer);
            return Save();
        }
    }
}
