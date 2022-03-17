using CustomerAPI.Interfaces;
using CustomerAPI.Models;
using CustomerAPI.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerAPI.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly CompanyDBContext _context;

        public ProductRepository(CompanyDBContext context) => _context = context;

        public bool CreateProduct(Product product)
        {
            _context.Add(product);
            return Save();
        }

        public bool Delete(Product product)
        {
            _context.Remove(product);
            return Save();
        }

        public ICollection<Product> GetAllProducts()
        {
            return _context.Products.ToList();
        }

        public Product GetProductById(int id)
        {
            return _context.Products.Where(p => p.Id == id).FirstOrDefault();
        }

        public bool ProductExists(int id)
        {
            return _context.Products.Any(c => c.Id == id);
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateProduct(Product product)
        {
            _context.Update(product);
            return Save();
        }
    }
}
