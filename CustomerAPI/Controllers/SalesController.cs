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
    public class SalesController : ControllerBase
    {
        private readonly ISaleRepository _salesRepository;
        private readonly IMapper mapper;

        public SalesController(ISaleRepository salesRepository, IMapper mapper)
        {
            _salesRepository = salesRepository;
            this.mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetSales()
        {
            var sales = mapper.Map<List<SaleDTO>>(_salesRepository.GetAllSales());

            if (!ModelState.IsValid)
                return BadRequest();

            return Ok(sales);

        }

        [HttpGet("{id}", Name = "GetSales")]
        public IActionResult GetSaleById(int id)
        {
            if (!_salesRepository.SaleExists(id))
                return NotFound();

            var sal = mapper.Map<CustomerDTO>(_salesRepository.GetSaleById(id));

            if (!ModelState.IsValid)
                return BadRequest();

            return Ok(sal);
        }

        [HttpPost]
        public IActionResult PostSale([FromBody] SaleDTO saleCreate)
        {
            if (saleCreate == null)
                return BadRequest();

            //Run a case insensitive test
            var customer = _salesRepository.GetAllSales()
                .Where(c => c.Code.Trim().ToUpper() == saleCreate.Code.TrimEnd().ToUpper())
                .FirstOrDefault();

            if (!ModelState.IsValid)
                return BadRequest();

            var saleMap = mapper.Map<Sale>(saleCreate);

            _salesRepository.CreateSale(saleMap);

            return Ok("Successfully Created");
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] SaleDTO saleUpdate)
        {
            if (saleUpdate == null)
            {
                return BadRequest(ModelState);
            }

            var salMap = mapper.Map<Sale>(saleUpdate);

            if (id != saleUpdate.Id)
            {
                return NotFound();
            }

            saleUpdate.Code = saleUpdate.Code;
            saleUpdate.Amount = saleUpdate.Amount;
            saleUpdate.ProductId = saleUpdate.ProductId;
            saleUpdate.CustomerId = saleUpdate.CustomerId;

            _salesRepository.UpdateSale(salMap);
            return Ok("Successfully Updated");
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteSale(int id)
        {
            var _sale = _salesRepository.GetSaleById(id);
            if (_sale == null)
            {
                return NotFound();
            }
            _salesRepository.Delete(_sale);
            return Ok("Successfully Deleted");
        }
    }
}
