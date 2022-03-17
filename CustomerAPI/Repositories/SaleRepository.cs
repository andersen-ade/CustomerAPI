using CustomerAPI.Interfaces;
using CustomerAPI.Models;
using CustomerAPI.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompanyAPI.Repositories
{
    public class SaleRepository : ISaleRepository
    {
        private readonly CompanyDBContext context;

        public SaleRepository(CompanyDBContext context) => this.context = context;

        public bool CreateSale(Sale sale)
        {
            context.Add(sale);
            return Save();
        }

        public bool Delete(Sale sale)
        {
            context.Remove(sale);
            return Save();
        }

        public ICollection<Sale> GetAllSales()
        {
            return context.Sales.ToList();
        }

        public Sale GetSaleById(int id)
        {
            return context.Sales.Where(s => s.Id == id).FirstOrDefault();

        }

        public bool Save()
        {
            var saved = context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateSale(Sale sale)
        {
            context.Update(sale);
            return Save();
        }

        public bool SaleExists(int id)
        {
            return context.Sales.Any(v => v.Id == id);
        }
    }
}
