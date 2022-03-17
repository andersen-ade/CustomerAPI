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
    public class VendorRepository : IVendorRepository
    {
        private readonly CompanyDBContext context;

        public VendorRepository(CompanyDBContext context)
        {
            this.context = context;
        }

        public bool CreateVendor(Vendor vendor)
        {
            context.Add(vendor);
            return Save();
        }

        public bool Delete(Vendor vendor)
        {
            context.Remove(vendor);
            return Save();
        }

        public async Task<IEnumerable<Vendor>> GetAll()
        {
            return await context.Vendors.Include(b => b.Products)
                                        .ToListAsync();
        }

        public ICollection<Vendor> GetAllVendors()
        {
            return context.Vendors.ToList();
        }

        public Vendor GetVendorById(int id)
        {
            return context.Vendors.Where(v => v.Id == id).FirstOrDefault();
            
        }

        public bool Save()
        {
            var saved = context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateVendor(Vendor vendor)
        {
            context.Update(vendor);
            return Save();
        }

        public bool VendorExists(int id)
        {
            return context.Vendors.Any(v => v.Id == id);
        }
    }
}
