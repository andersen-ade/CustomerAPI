using CustomerAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerAPI.Interfaces
{
    public interface IProductRepository
    {
        ICollection<Product> GetAllProducts();
        Product GetProductById(int id);
        bool ProductExists(int id);
        bool CreateProduct(Product product);
        bool UpdateProduct(Product product);
        bool Delete(Product product);
        bool Save();
    }
}
