using CustomerAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerAPI.Interfaces
{
    public interface IVendorRepository
    {
        ICollection<Vendor> GetAllVendors();
        Vendor GetVendorById(int id);
        bool VendorExists(int id);
        bool CreateVendor(Vendor vendor);
        bool UpdateVendor(Vendor vendor);        
        bool Delete(Vendor vendor);
        bool Save();
    }
}
