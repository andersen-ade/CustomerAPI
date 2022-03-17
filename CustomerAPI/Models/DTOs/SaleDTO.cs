using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerAPI.Models.DTOs
{
    public class SaleDTO
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public double Amount { get; set; }
        public int? CustomerId { get; set; }
        public int? ProductId { get; set; }
    }
}
