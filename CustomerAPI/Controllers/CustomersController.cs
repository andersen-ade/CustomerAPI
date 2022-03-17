using CustomerAPI.Models;
using CustomerAPI.Models.DTOs;
using CustomerAPI.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using CustomerAPI.Interfaces;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;

namespace CustomerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : Controller
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IMapper mapper;

        public CustomersController(ICustomerRepository customerRepository, IMapper mapper)
        {
            _customerRepository = customerRepository;
            this.mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetCustomers()
        {
            var customers = mapper.Map<List<CustomerDTO>>(_customerRepository.GetAllCustomers());

            if (!ModelState.IsValid)
                return BadRequest();

            return Ok(customers);

        }

        [HttpGet("{id}", Name = "GetCustomer")]
        public IActionResult GetCustomerById(int id)
        {
            if (!_customerRepository.CustomerExists(id))
                return NotFound();

            var cus = mapper.Map<CustomerDTO>(_customerRepository.GetCustomerById(id));

            if (!ModelState.IsValid)
                return BadRequest();
         
            return Ok(cus);
        }

        [HttpPost]
        public IActionResult PostCustomer([FromBody] CustomerDTO customerCreate)
        {
            if (customerCreate == null)
                return BadRequest();

            //Run a case insensitive test
            var customer = _customerRepository.GetAllCustomers()
                .Where(c => c.FirstName.Trim().ToUpper() == customerCreate.FirstName.TrimEnd().ToUpper())
                .FirstOrDefault();
            
            if (!ModelState.IsValid)
                return BadRequest();

            var customerMap = mapper.Map<Customer>(customerCreate);

            _customerRepository.CreateCustomer(customerMap);

            return Ok("Successfully Created");
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] CustomerDTO customerUpdate)
        {
            if (customerUpdate == null)
            {
                return BadRequest(ModelState);
            }

            var cusMap = mapper.Map<Customer>(customerUpdate);

            if (id != customerUpdate.Id)
            {
                return NotFound();
            }

            customerUpdate.FirstName = customerUpdate.FirstName;
            customerUpdate.LastName = customerUpdate.LastName;
            customerUpdate.Gender = customerUpdate.Gender;
            customerUpdate.Email = customerUpdate.Email;

            _customerRepository.UpdateCustomer(cusMap);
            return Ok("Successfully Updated");
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteCustomer(int id)
        {
            var _customer = _customerRepository.GetCustomerById(id);
            if (_customer == null)
            {
                return NotFound();
            }
            _customerRepository.DeleteCustomer(_customer);
            return Ok("Successfully Deleted");
        }
    }
}
