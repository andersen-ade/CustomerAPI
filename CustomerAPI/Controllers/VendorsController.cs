using AutoMapper;
using CustomerAPI.Interfaces;
using CustomerAPI.Models;
using CustomerAPI.Models.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VendorsController : ControllerBase
    {
        private readonly IVendorRepository vendorRepository;
        private readonly IMapper mapper;

        public VendorsController(IVendorRepository vendorRepository, IMapper mapper)
        {
            this.vendorRepository = vendorRepository;
            this.mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetVendors()
        {
            var vendors = mapper.Map<List<VendorDTO>>(vendorRepository.GetAllVendors());

            if (!ModelState.IsValid)
                return BadRequest();

            return Ok(vendors);
        }

        [HttpGet("{id}")]
        public IActionResult GetVendor(int id)
        {
            if (!vendorRepository.VendorExists(id))
                return NotFound();

            var ven = mapper.Map<VendorDTO>(vendorRepository.GetVendorById(id));

            if (!ModelState.IsValid)
                return BadRequest();

            return Ok(ven);
        }

        [HttpPost]
        public IActionResult PostCustomer([FromBody] VendorDTO vendorCreate)
        {
            if (vendorCreate == null)
                return BadRequest();

            var vend = vendorRepository.GetAllVendors()
                .Where(v => v.Name.Trim().ToUpper() == vendorCreate.Name.TrimEnd().ToUpper())
                .FirstOrDefault();

            if (!ModelState.IsValid)
                return BadRequest();

            var vendorMap = mapper.Map<Vendor>(vendorCreate);

            vendorRepository.CreateVendor(vendorMap);

            return Ok("Successfully Created");
        }

        [HttpPut("{id}")]
        public IActionResult EditVendor(int id, [FromBody] VendorDTO vendorUpdate)
        {
            if (vendorUpdate == null)
            {
                return BadRequest(ModelState);
            }

            var venMap = mapper.Map<Vendor>(vendorUpdate);

            if (id != vendorUpdate.Id)
            {
                return NotFound();
            }

            vendorUpdate.Name = vendorUpdate.Name;
            vendorUpdate.Email = vendorUpdate.Email;
            vendorUpdate.Address = vendorUpdate.Address;
            

            vendorRepository.UpdateVendor(venMap);
            return Ok("Successfully Updated");
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteVendor(int id)
        {
            var _vendor = vendorRepository.GetVendorById(id);
            if (_vendor == null)
            {
                return NotFound();
            }
            vendorRepository.Delete(_vendor);
            return Ok("Successfully Deleted");
        }

    }
}
