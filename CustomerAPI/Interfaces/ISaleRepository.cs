using CustomerAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerAPI.Interfaces
{
    public interface ISaleRepository
    {
        ICollection<Sale> GetAllSales();
        Sale GetSaleById(int id);
        bool SaleExists(int id);
        bool CreateSale(Sale sale);
        bool UpdateSale(Sale sale);
        bool Delete(Sale sale);
        bool Save();
    }
}
