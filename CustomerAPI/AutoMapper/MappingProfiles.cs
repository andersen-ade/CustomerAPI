using AutoMapper;
using CustomerAPI.Models;
using CustomerAPI.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerAPI.AutoMapper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Customer, CustomerDTO>();
            CreateMap<Vendor, VendorDTO>();
            CreateMap<Product, ProductDTO>();
            CreateMap<Sale, SaleDTO>();

            CreateMap<CustomerDTO, Customer>();
            CreateMap<VendorDTO, Vendor>();
            CreateMap<ProductDTO, Product>();
            CreateMap<SaleDTO, Sale>();
        }
    }
}
