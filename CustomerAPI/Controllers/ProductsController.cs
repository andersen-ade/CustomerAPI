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
    public class ProductsController : ControllerBase
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper mapper;

        public ProductsController(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            this.mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetProducts()
        {
            var products = mapper.Map<List<ProductDTO>>(_productRepository.GetAllProducts());

            if (!ModelState.IsValid)
                return BadRequest();

            return Ok(products);
        }

        [HttpGet("{id}", Name = "GetProduct")]
        public IActionResult GetProductById(int id)
        {
            if (!_productRepository.ProductExists(id))
                return NotFound();

            var prod = mapper.Map<ProductDTO>(_productRepository.GetProductById(id));

            if (!ModelState.IsValid)
                return BadRequest();

            return Ok(prod);
        }

        [HttpPost]
        public IActionResult PostProduct([FromBody] ProductDTO productCreate)
        {
            if (productCreate == null)
                return BadRequest();

            //Run a case insensitive test
            var product = _productRepository.GetAllProducts()
                .Where(c => c.Name.Trim().ToUpper() == productCreate.Name.TrimEnd().ToUpper())
                .FirstOrDefault();

            if (!ModelState.IsValid)
                return BadRequest();

            var productMap = mapper.Map<Product>(productCreate);

            _productRepository.CreateProduct(productMap);

            return Ok("Successfully Created");
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] ProductDTO productUpdate)
        {
            if (productUpdate == null)
            {
                return BadRequest(ModelState);
            }

            var prodMap = mapper.Map<Product>(productUpdate);

            if (id != productUpdate.Id)
            {
                return NotFound();
            }

            productUpdate.Name = productUpdate.Name;
            productUpdate.Available = productUpdate.Available;
            productUpdate.Code = productUpdate.Code;
            productUpdate.VendorId = productUpdate.VendorId;

            _productRepository.UpdateProduct(prodMap);
            return Ok("Successfully Updated");
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteProduct(int id)
        {
            var _product = _productRepository.GetProductById(id);
            if (_product == null)
            {
                return NotFound();
            }
            _productRepository.Delete(_product);
            return Ok("Successfully Deleted");
        }
    }
}
